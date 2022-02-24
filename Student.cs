namespace CQRSWebApplication
{
    public class Student
    {
        private long _id;
        private string _name { get; set; }
        private string _email { get; set; }
        internal List<Enrollment> _enrollments = new();
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
            var disenrollment = new Disenrollment(enrollment.GetStudent(), enrollment.GetCourse(), comment);
            _disenrollments.Add(disenrollment);
        }

        public List<Enrollment> GetEnrollments()
        {
            return _enrollments;
        }

        public void Enroll(Course course, Grade grade)
        {
            if (_enrollments.Count >= 2)
            {
                throw new Exception("Cannot have more than 2 enrollments");
            }

            Enrollment enrollment = new Enrollment(this, course, grade);
            _enrollments.Add(enrollment);
        }

        public void SetName(string getName)
        {
            throw new NotImplementedException();
        }

        public void SetEmail(string getEmail)
        {
            throw new NotImplementedException();
        }

        public Enrollment GetFirstEnrollment()
        {
            throw new NotImplementedException();
        }

        public Enrollment GetSecondEnrollment()
        {
            throw new NotImplementedException();
        }

        public void AddDisenrollmentComment(Enrollment enrollment, string getCourse1DisenrollmentComment)
        {
            throw new NotImplementedException();
        }
    }
}