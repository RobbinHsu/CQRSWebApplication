namespace CQRSWebApplication;

public class Disenrollment
{
    private long id;
    private Student _student;
    private Course _course;
    private DateTime _dateTime;
    private String _comment;

    public Disenrollment(Student student, Course course,
        String comment)
    {
        this._student = student;
        this._course = course;
        this._comment = comment;
        this._dateTime = new DateTime();
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