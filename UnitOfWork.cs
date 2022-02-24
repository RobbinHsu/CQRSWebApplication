namespace CQRSWebApplication;

public class UnitOfWork
{
    public IEnumerable<T> Query<T>()
    {
        throw new NotImplementedException();
    }

    public void Commit()
    {
        throw new NotImplementedException();
    }

    public T Get<T>(long id)
    {
        throw new NotImplementedException();
    }

    public void SaveOrUpdate(Student student)
    {
        throw new NotImplementedException();
    }

    public void Delete(Student student)
    {
        throw new NotImplementedException();
    }
}