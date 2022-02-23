namespace CQRSWebApplication;

public class Disenrollments
{
    public Course Course { get; set; }
    public DateTime DateTime { get; set; }
    public string Comment { get; set; }
    public Disenrollments Disenrollments1 { get; set; } 
}