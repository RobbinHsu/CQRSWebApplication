namespace CQRSWebApplication
{
    public class Student
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Enrollments Enrollments { get; set; }
        public Disenrollments Disenrollments { get; set; }
    }
}
