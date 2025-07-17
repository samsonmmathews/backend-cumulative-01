using Backend_Cumulative_01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Reflection.Metadata.Ecma335;

namespace Backend_Cumulative_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        // get access to the blog object
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// This method should connect to the database and extract the teachers' names
        /// </summary>
        /// <returns>
        /// A list of teachers' names
        /// </returns>
        /// <example>
        /// 
        /// </example>
        [HttpGet(template: "ListTeacherNames")]
        public List<string> ListTeacherNames()
        {
            // Creates an empty list of teacher names
            List<string> TeacherNames = new List<string>();

            // Create the connection to the database
            MySqlConnection Connection = School.AccessDatabase();

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
            MySqlDataReader ResultSet = Command.ExecuteReader();

            // loop through the results one at a time
            while(ResultSet.Read())
            {
                // add the first names one at a time
                string TeacherName = ResultSet["teacherfname"].ToString();
                TeacherNames.Add(TeacherName);
            }

            // add the teachers one at a time

            //return the teachers' names
            return TeacherNames;
        }
    }
}
