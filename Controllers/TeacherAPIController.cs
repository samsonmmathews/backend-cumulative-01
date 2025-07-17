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

            // Open the connection to the database
            MySqlConnection Connection =

            // Write a query - "select * from schooldb"

            // "run" the query against the database

            // get the response from the database as a "Result Set"

            // loop through the results one at a time

            // add the teachers one at a time

            //return the teachers' names
            return TeacherNames;
        }
    }
}
