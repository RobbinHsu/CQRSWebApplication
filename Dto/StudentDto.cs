namespace CQRSWebApplication.Dto;

public class StudentDto
{
    private long id;
    public string Name;
    public string Email;
    public string Course1;
    public Grade Course1Grade;
    public string Course1DisenrollmentComment;
    public int Course1Credits;
    public string Course2;
    public Grade Course2Grade;
    public string Course2DisenrollmentComment;
    public int Course2Credits;

    //getters & setters

    public string GetCourse1Grade()
    {
        throw new Exception();
    }

    public string GetCourse1()
    {
        return Course1;
    }

    public string GetCourse2Grade()
    {
        throw new NotImplementedException();
    }

    public string GetCourse2()
    {
        throw new NotImplementedException();
    }

    public string GetName()
    {
        throw new NotImplementedException();
    }

    public string GetEmail()
    {
        throw new NotImplementedException();
    }

    public string GetCourse1DisenrollmentComment()
    {
        throw new NotImplementedException();
    }

    public string GetCourse2DisenrollmentComment()
    {
        throw new NotImplementedException();
    }
}