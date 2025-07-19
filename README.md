# Backend_Cumulative_01

This assignment connects our server to a MySQL Database with MySql.Data.MySqlClient.

/Models/SchoolDbContext.cs - A class which connects to your MySQL database
/Controllers/TeacherAPIController.cs - A WebAPI Controller which allows you to access information about teachers
/Controllers/TeacherPageController.cs - An MVC Controller which allows you to route to dynamic pages
/Models/Teacher.cs - A Model which allows you to represent information about a teacher
/Views/Teacher/List.cshtml - A View which uses server rendering to display a list of teachers from the MySQL Database
/Views/Teacher/Show.cshtml - A View which uses server rendering to display a teacher from the MySQL Database