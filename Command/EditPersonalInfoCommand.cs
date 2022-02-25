using System.Windows.Input;

namespace CQRSWebApplication.Command;

public class EditPersonalInfoCommand : ICQRSCommand
{
    public long id;
    public string name;

    public string email;

    //setters & getters
    public void SetEmail(string getEmail)
    {
        throw new NotImplementedException();
    }

    public void SetName(string getName)
    {
        throw new NotImplementedException();
    }

    public void SetId(long l)
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

    public long GetId()
    {
        throw new NotImplementedException();
    }
}

public class ICQRSCommand
{
}