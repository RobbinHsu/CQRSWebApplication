namespace CQRSWebApplication.Repository;

public class StudentRepository
{
    private UnitOfWork _unitOfWork;

    public StudentRepository(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<Student> GetList(string enrolledIn, int numberOfCourses)
    {
        IEnumerable<Student> query = _unitOfWork.Query<Student>();

        if (!string.IsNullOrEmpty(enrolledIn))
        {
            query = query.Where(x => x._enrollments.Any(
                e => e.GetCourse().GetName() == enrolledIn));
        }

        List<Student> result = query.ToList();

        if (numberOfCourses != null)
        {
            result = result.Where(x =>
                x.GetEnrollments().Count() == numberOfCourses).ToList();
        }

        return result;
    }

    public Student GetById(long id)
    {
        return _unitOfWork.Get<Student>(id);
    }

    public void Save(Student student)
    {
        _unitOfWork.SaveOrUpdate(student);
    }

    public void Delete(Student student)
    {
        _unitOfWork.Delete(student);
    }
}