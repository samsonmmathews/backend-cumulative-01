namespace Backend_Cumulative_01.Models
{
    public class Teacher
    {
        // A Teacher ID primary key 
        public int TeacherId { get; set; }
        // the first name of the teacher
        public string TeacherFname { get; set; }
        // the last name of the teacher
        public string TeacherLname { get; set; }
        // the employee number of the teacher
        public string EmployeeNumber { get; set; }
        // the hire date of the teacher
        public DateTime HireDate { get; set; }
        // the salary of the teacher
        public decimal Salary { get; set; }
    }
}
