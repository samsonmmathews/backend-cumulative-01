using Backend_Cumulative_01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.IO.Pipelines;
using System.Reflection.Metadata.Ecma335;

namespace Backend_Cumulative_01.Controllers
{
    [Route("api/Teacher")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        // get access to the blog object
        private SchoolDbContext _context = new SchoolDbContext();

        /// <summary>
        /// This method should connect to the database and extract the details of all teachers
        /// </summary>
        /// <returns>
        /// Information on all teachers
        /// </returns>
        /// <example>
        /// 
        /// </example>
        [HttpGet(template: "ListTeachers")]
        public List<Teacher> ListTeachers()
        {
            // Creates an empty list of teacher names
            List<Teacher> TeacherNames = new List<Teacher>();

            // Create the connection to the database
            using (MySqlConnection Connection = _context.AccessDatabase())
            {

                // Open the connection to the database
                Connection.Open();

                // Write a query - "select * from schooldb"
                string query = "select * from teachers";

                // create a command
                MySqlCommand Command = Connection.CreateCommand();

                // set the command text to the query
                Command.CommandText = query;

                // run the command against the database
                // get the response from the database as a "Result Set"
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    // loop through the results one at a time
                    while (ResultSet.Read())
                    {
                        Teacher TeacherData = new Teacher();
                        TeacherData.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        TeacherData.TeacherFname = ResultSet["teacherfname"].ToString();
                        TeacherData.TeacherLname = ResultSet["teacherlname"].ToString();
                        TeacherData.EmployeeNumber = ResultSet["employeenumber"].ToString();
                        TeacherData.HireDate = DateTime.Parse(ResultSet["hiredate"].ToString());
                        TeacherData.Salary = decimal.Parse(ResultSet["salary"].ToString());
                        TeacherNames.Add(TeacherData);
                    }
                }
            }

            //return the teachers' names
            return TeacherNames;
        }
    }
}
