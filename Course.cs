namespace CQRSWebApplication;

public class Course
{
    private long _id;
    private string _name { get; set; }
    private int _credits { get; set; }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public int GetCredits()
    {
        return _credits;
    }

    public void SetCredits(int credits)
    {
        _credits = credits;
    }
}