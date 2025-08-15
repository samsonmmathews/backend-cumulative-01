using Microsoft.AspNetCore.Mvc;
using Backend_Cumulative_01.Models;
using System.Diagnostics;

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

        // GET: TeacherPage/List?SearchKey={SearchKey} ->
        // Response Headers:
        // Content-Type: application/xml
        // Content: html document with article as links
        // A webpage showing all teachers in the database
        // matching the optional search key input
        [HttpGet]
        public IActionResult List(string SearchKey)
        {
            Debug.WriteLine("The search key is " + SearchKey);
            // Returns the list of all teachers
            List<Teacher> Teachers = _api.ListTeachers(SearchKey);

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
            // Get the teacher with the teacher id

            Teacher SelectedTeacher = _api.SpecificTeacher(id);


            // Mock Testing
            //SelectedTeacher.TeacherId = 1;

            //SelectedTeacher.TeacherFname = "First name of the teacher";

            //SelectedTeacher.TeacherLname = "Last name of the teacher";

            // Directs to /Views/TeacherPage/Show.cshtml

            return View(SelectedTeacher);
        }   

        // GET: TeacherPage/New -> A webpage that asks the user for the new teacher information
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        // POST: TeacherPage/Create
        // Headers:
        // application/x-www-form-unlocked
        // Request Body: &TeacherFname={TeacherFname}&TeacherLname={TeacherLname}
        // ->Adds teacher and directs to List.cshtml
        [HttpPost]
        public IActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            Debug.WriteLine("Form submitted");
            Debug.WriteLine($"{TeacherFname} {TeacherLname} {EmployeeNumber} {HireDate} {Salary}");
            // Directs to /TeacherPage/List.cshtml

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            int TeacherId = _api.AddTeacher(NewTeacher);

            // Directs to /TeacherPage/Show/{id}
            return RedirectToAction("Show", new {id = TeacherId });
        }

        // GET : /TeacherPage/DeleteConfirm/{id} -> A webpage that asks the user
        // if they want to delete this article

        public IActionResult DeleteConfirm(int id)
        {
            Teacher SelectedTeacher = _api.SpecificTeacher(id);
            //Directs to /Views/TeacherPage/DeleteConfirm.cshtml
            return View(SelectedTeacher);
        }

        // POST: /TeacherPage/Delete/{id} -> Deletes the teacher and returns to the List.cshtml
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _api.DeleteTeacher(id);
            // Directs to /TeacherPage/List.cshtml
            return RedirectToAction("List");
        }

        // GET: /TeacherPage/Edit/{id} -> A webpage that asks the user what fields of a teacher to update
        public IActionResult Edit(int id)
        {
            // Given: The teacher id
            Teacher SelectedTeacher = _api.SpecificTeacher(id);
            return View(SelectedTeacher);
        }

        // POST: /TeacherPage/Update/{id}
        // FORM DATA:
        // Headers: Content-Type application/x-www-form-urlencoded
        // TeacherFname={TeacherFname}&TeacherLname={TeacherLname}
        [HttpPost]
        public IActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            Debug.WriteLine("Teacher" + id);
            Debug.WriteLine("First name: " + TeacherFname);
            Debug.WriteLine("Last name: " + TeacherLname);
            Debug.WriteLine("Employee Number: " + EmployeeNumber);
            Debug.WriteLine("Hire Date: " + HireDate);
            Debug.WriteLine("Salary: " + Salary);

            Teacher UpdatedTeacher = new Teacher();

            UpdatedTeacher.TeacherFname = TeacherFname;
            UpdatedTeacher.TeacherLname = TeacherLname;
            UpdatedTeacher.EmployeeNumber = EmployeeNumber;
            UpdatedTeacher.HireDate = HireDate;
            UpdatedTeacher.Salary = Salary;

            _api.UpdateTeacher(id, UpdatedTeacher);

            // Redirects to /TeacherPage/Show/{id}
            return RedirectToAction("Show", new { id = id });
        }
    }
}
