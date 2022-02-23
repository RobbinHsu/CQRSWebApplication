namespace CQRSWebApplication
{
    public class Student
    {
        private long _id;
        private string _name { get; set; }
        private string _email { get; set; }
        private List<Enrollment> _enrollments = new();
        private List<Disenrollment> _disenrollments = new();

        public Student(string name, string email)
        {
            _name = name;
            _email = email;
        }

        private Enrollment GetEnrollment(int index)
        {
            if (_enrollments.Count > index)
            {
                return _enrollments[index];
            }

            return null;
        }

        public void RemoveEnrollment(Enrollment enrollment)
        {
            _enrollments.Remove(enrollment);
        }

        public void AddDisenrollment(Enrollment enrollment, string comment)
        {
            var disenrollment = new Disenrollment(enrollment.GetStudent(),enrollment.GetCourse(), comment);
            _disenrollments.Add(disenrollment);
        }
    }
}