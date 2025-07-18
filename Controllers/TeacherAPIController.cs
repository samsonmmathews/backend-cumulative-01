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
        /// GET : api/Teacher/ListTeachers -> [{"teacherId":1,"teacherFname":"Alexander",
        /// "teacherLname":"Bennett","employeeNumber":"T378",
        /// "hireDate":"2016-08-05T00:00:00","salary":55.30}]
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

                // Write a query - "select * from teachers"
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

            //return the details of all the teachers
            return TeacherNames;
        }

        /// <summary>
        /// This method should connect to the database and extract the details of a specific teacher
        /// </summary>
        /// <returns>
        /// Information on a specific teacher
        /// </returns>
        /// <example>
        /// GET : api/Teacher/SpecificTeacher/7 -> {"teacherId":7,"teacherFname":"Shannon","teacherLname":"Barton",
        /// "employeeNumber":"T397","hireDate":"2013-08-04T00:00:00","salary":64.70}
        /// </example>
        /// <example>
        /// GET : api/Teacher/SpecificTeacher/24 -> {"teacherId":0,"teacherFname":null,"teacherLname":null,
        /// "employeeNumber":null,"hireDate":"0001-01-01T00:00:00","salary":0}
        /// </example>
        /// GET : api/Teacher/SpecificTeacher/hey -> {"type":"https://tools.ietf.org/html/rfc9110#section-15.5.1",
        /// "title":"One or more validation errors occurred.","status":400,"errors":{"inputId":["The value 'hey' is not valid."]},
        /// "traceId":"00-a42745faed81e05c6381e7d9c8e0987c-38b6352bac25c7e2-00"}
        /// </example>
        [HttpGet(template: "SpecificTeacher/{inputId}")]
        public Teacher SpecificTeacher(int inputId)
        {
            // Creates an empty list to store the teacher info
            Teacher SpecificTeacherInfo = new Teacher();

            // Create the connection to the database
            using (MySqlConnection Connection = _context.AccessDatabase())
            {

                // Open the connection to the database
                Connection.Open();

                // Write a query - $"select * from teachers where teacherid = {inputId}"
                string query = $"select * from teachers where teacherid = {inputId}";

                // create a command
                MySqlCommand Command = Connection.CreateCommand();

                // set the command text to the query
                Command.CommandText = query;

                // run the command against the database
                // get the response from the database as a "Result Set"
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    // error handling code - checks if the teacher id exists
                    if(ResultSet.Read())
                    {
                        SpecificTeacherInfo.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        SpecificTeacherInfo.TeacherFname = ResultSet["teacherfname"].ToString();
                        SpecificTeacherInfo.TeacherLname = ResultSet["teacherlname"].ToString();
                        SpecificTeacherInfo.EmployeeNumber = ResultSet["employeenumber"].ToString();
                        SpecificTeacherInfo.HireDate = DateTime.Parse(ResultSet["hiredate"].ToString());
                        SpecificTeacherInfo.Salary = decimal.Parse(ResultSet["salary"].ToString());
                    }
                }
            }
            //return the details of the teacher
            return SpecificTeacherInfo;
        }
    }
}
