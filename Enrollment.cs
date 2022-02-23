namespace CQRSWebApplication;

public class Enrollment
{
    private long _id;
    private Student _student;
    private Course _course;
    private Grade _grade;

    protected Enrollment()
    {
    }

    public Enrollment(Student student, Course course, Grade grade)
    {
        _student = student;
        _course = course;
        _grade = grade;
    }

    public Student GetStudent()
    {
        return _student;
    }

    public Course GetCourse()
    {
        return _course;
    }
}