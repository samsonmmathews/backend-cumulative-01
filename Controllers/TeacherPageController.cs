using Microsoft.AspNetCore.Mvc;
using Backend_Cumulative_01.Models;

namespace Backend_Cumulative_01.Controllers
{
    public class TeacherPageController : Controller
    {
        // This file is meant to show web pages about teachers

        private TeacherAPIController _api;
        // Dependency Injection
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        // GET: TeacherPage/List ->
        // Response Headers:
        // Content-Type: application/xml
        // Content: html document with article as links
        // A webpage showing all teachers as links
        [HttpGet]
        public IActionResult List()
        {
            List<Teacher> Teachers = _api.ListTeachers();

            // Mock testing - three dummy teachers' details

            //Teacher Teacher1 = new Teacher();
            //Teacher1.TeacherId = 1;
            //Teacher1.TeacherFname = "ABC";
            //Teacher1.TeacherLname = "DEF";
            //Teacher1.EmployeeNumber = "faegsbdn";

            //Teacher Teacher2 = new Teacher();
            //Teacher2.TeacherId = 2;
            //Teacher2.TeacherFname = "FGHJ";
            //Teacher2.TeacherLname = "JTHK";
            //Teacher2.EmployeeNumber = "sgbhdng";

            //Teacher Teacher3 = new Teacher();
            //Teacher3.TeacherId = 3;
            //Teacher3.TeacherFname = "ERHJ";
            //Teacher3.TeacherLname = "EFGBN";
            //Teacher3.EmployeeNumber = "sgfhsf";

            //Teachers.Add(Teacher1);
            //Teachers.Add(Teacher2);
            //Teachers.Add(Teacher3);

            // directs to /Views/Teacher/List.cshtml
            return View(Teachers);
        }

        // GET: TeacherPage/Show/{id} -> A webpage showing more information
        // about a particular teacher

        [HttpGet]
        public IActionResult Show(int id)
        {
            // get the article with the input id

            Teacher SelectedTeacher = _api.SpecificTeacher(id);


            // Mock Testing
            //SelectedTeacher.TeacherId = 1;

            //SelectedTeacher.TeacherFname = "First name of the teacher";

            //SelectedTeacher.TeacherLname = "Last name of the teacher";

            // Directs to /Views/TeacherPage/Show.cshtml

            return View(SelectedTeacher);
        }
    }
}
