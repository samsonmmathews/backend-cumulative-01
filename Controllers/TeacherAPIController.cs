using Backend_Cumulative_01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.IO.Pipelines;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;

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
        /// <param name="SearchKey">The search key to search for
        /// teachers by their name</param>
        /// <example>
        /// GET : api/TeacherAPI/ListTeachers -> [{"teacherId":1,"teacherFname":"Alexander",
        /// "teacherLname":"Bennett","employeeNumber":"T378",
        /// "hireDate":"2016-08-05T00:00:00","salary":55.30}]
        /// </example>
        /// <example>
        /// GET : api/TeacherAPI/ListTeachers?SearchKey=Alexander -> [{"teacherId":1,"teacherFname":"Alexander",
        /// "teacherLname":"Bennett","employeeNumber":"T378",
        /// "hireDate":"2016-08-05T00:00:00","salary":55.30}]
        /// </example>
        [HttpGet(template: "ListTeachers")]
        public List<Teacher> ListTeachers(string? SearchKey)
        {
            Debug.WriteLine("API Received search key " + SearchKey);
            // Creates an empty list of teacher names
            List<Teacher> TeacherNames = new List<Teacher>();

            // Create the connection to the database
            using (MySqlConnection Connection = _context.AccessDatabase())
            {

                // Open the connection to the database
                Connection.Open();

                // create a command
                MySqlCommand Command = Connection.CreateCommand();

                // Write a query - "select * from teachers"
                string query = "SELECT teachers.* from teachers where teachers.teacherfname LIKE @key OR teachers.teacherlname LIKE @key";

                // set the command text to the query
                Command.CommandText = query;
                Command.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
                Command.Prepare(); 

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
                // Write a query - $"select * from teachers where teacherid = {inputId}"
                string query = $"select * from teachers where teacherid = @id";

                // Open the connection to the database
                Connection.Open();

                // create a command
                MySqlCommand Command = Connection.CreateCommand();

                Command.Parameters.AddWithValue("@id", inputId);

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

        /// <summary>
        /// This method receives information on teacher and adds
        /// a teacher to the database
        /// </summary>
        /// <example>
        /// POST: api/Teacher/AddTeacher
        /// HEADERS
        /// Content-Type: application/json
        /// FORM DATA / REQUEST BODY / POST DATA:
        /// {"teacherId":7,"teacherFname":"Shannon","teacherLname":"Barton",
        /// "employeeNumber":"T397","hireDate":"2013-08-04T00:00:00","salary":64.70}
        /// ->
        /// T100
        /// </example>
        /// <returns>
        /// Returns the teacher ID that was inserted into the database, -1 if there is an error
        /// </returns>
        [HttpPost(template:"AddTeacher")]
        public int AddTeacher([FromBody]Teacher NewTeacher)
        {
            // For testing
            Debug.WriteLine($"Teacher TeacherFname {NewTeacher.TeacherFname}");
            Debug.WriteLine($"Teacher TeacherLname {NewTeacher.TeacherLname}");
            
            // Write a query
            string query = "insert into teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) values (0, @TeacherFname, @TeacherLname, @EmployeeNumber, CURRENT_DATE(), @Salary)";

            int TeacherId = -1;
            using (MySqlConnection Conn = _context.AccessDatabase())
            {
                // Open the connection to the database
                Conn.Open();

                MySqlCommand Command = Conn.CreateCommand();
                Command.CommandText = query;
                Command.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
                Command.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
                Command.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
                Command.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

                Command.ExecuteNonQuery();
                TeacherId = Convert.ToInt32(Command.LastInsertedId);
            }
            return TeacherId;
        }

    }
}
