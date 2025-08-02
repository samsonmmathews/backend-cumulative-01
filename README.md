# Backend_Cumulative_01

This assignment connects our server to a MySQL Database with MySql.Data.MySqlClient.

/Models/SchoolDbContext.cs - A class which connects to your MySQL database
/Controllers/TeacherAPIController.cs - A WebAPI Controller which allows you to access information about teachers
/Controllers/TeacherPageController.cs - An MVC Controller which allows you to route to dynamic pages
/Models/Teacher.cs - A Model which allows you to represent information about a teacher
/Views/Teacher/List.cshtml - A View which uses server rendering to display a list of teachers from the MySQL Database
/Views/Teacher/Show.cshtml - A View which uses server rendering to display a teacher from the MySQL Database

# Backend_Cumulative_02

We are adding ADD and DELETE functionalities in Part 2.
ADD function is used through API and webpage.
DELETE function is also used through API and through webpage

We are also adding two new files:
/Views/Teacher/New.cshtml - A View which uses server rendering to display a page that allows for a user to enter a new teacher
/Views/Teacher/DeleteConfirm.cshtml - A View which uses server rendering to display a “Confirm Delete” page for a teacher from the MySQL Database