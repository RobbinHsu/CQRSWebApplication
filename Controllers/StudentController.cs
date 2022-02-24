using CQRSWebApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CQRSWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class StudentController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private UnitOfWork _unitOfWork;
        private StudentRepository studentRepository;
        private CourseRepository courseRepository;

        public StudentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            studentRepository = new StudentRepository(_unitOfWork);
            courseRepository = new CourseRepository(_unitOfWork);
        }

        //@GetMapping()
        public List<StudentDto> GetList(string enrolled, int number)
        {
            return ConvertToDtos(studentRepository.GetList(enrolled, number));
        }

        private List<StudentDto> ConvertToDtos(List<Student> students)
        {
            //convert student to studentDto
            throw new Exception();
        }

        //@PostMapping()
        public void Create([FromBody] StudentDto dto)
        {
            Student student = new Student(dto.Name, dto.Email);
            if (dto.Course1 != null && dto.GetCourse1Grade() != null)
            {
                Course course = courseRepository.GetByName(dto.GetCourse1());
                student.Enroll(course, dto.Course1Grade);
            }

            if (dto.Course2 != null &&
                dto.GetCourse2Grade() != null)
            {
                Course course =
                    courseRepository.GetByName(dto.GetCourse2());
                student.Enroll(course, dto.Course2Grade);
            }

            studentRepository.Save(student);
            _unitOfWork.Commit();
        }

        //@DeleteMapping("{id}")

        public void Delete(long id)
        {
            Student student = studentRepository.GetById(id);
            if (student == null)
            {
                throw new Exception($"No student found for Id {id}");
            }

            studentRepository.Delete(student);
            _unitOfWork.Commit();
        }

        //@PutMapping("{id}")

        public void Update(long id, [FromBody] StudentDto dto)
        {
            Student student = studentRepository.GetById(id);
            if (student == null)
            {
                throw new Exception($"No student found for Id {id}");
            }

            student.SetName(dto.GetName());
            student.SetEmail(dto.GetEmail());
            Enrollment firstEnrollment = student.GetFirstEnrollment();
            Enrollment secondEnrollment = student.GetSecondEnrollment();

            if (hasEnrollmentChanged(dto.GetCourse1(), dto.GetCourse1Grade(), firstEnrollment))
            {
                if (string.IsNullOrEmpty(dto.GetCourse1()))
                {
                    // Student disenrolls
                    if (string.IsNullOrEmpty(dto.GetCourse1DisenrollmentComment()))
                    {
                        throw new Exception("Disenrollment comment is required");
                    }

                    Enrollment enrollment = firstEnrollment;
                    student.RemoveEnrollment(enrollment);
                    student.AddDisenrollmentComment(enrollment, dto.GetCourse1DisenrollmentComment());
                }

                if (string.IsNullOrEmpty(dto.Course1Grade.ToString()))
                {
                    throw new Exception("Grade is required");
                }

                Course course = courseRepository.GetByName(dto.Course1);
                if (firstEnrollment == null)
                {
                    // Student enrolls
                    student.Enroll(course, dto.Course1Grade);
                }
                else
                {
                    // Student transfers
                    firstEnrollment.Update(course, dto.Course1Grade);
                }
            }

            if (hasEnrollmentChanged(dto.GetCourse2(), dto.GetCourse2Grade(), secondEnrollment))
            {
                if (string.IsNullOrEmpty(dto.Course2))
                {
                    //Student disenrolls
                    if (string.IsNullOrEmpty(dto.GetCourse2DisenrollmentComment()))
                    {
                        throw new Exception("Disenrollment comment is required");
                    }

                    Enrollment enrollment = secondEnrollment;
                    student.RemoveEnrollment(enrollment);
                    student.AddDisenrollmentComment(enrollment, dto.GetCourse2DisenrollmentComment());
                }

                if (string.IsNullOrEmpty(dto.GetCourse2Grade()))
                {
                    throw new Exception("Grade is required");
                }

                Course course = courseRepository.GetByName(dto.GetCourse2());
                if (secondEnrollment == null)
                {
                    // Student enrolls
                    student.Enroll(course, dto.Course2Grade);
                }
                else
                {
                    // Student transfers
                    secondEnrollment.Update(course, dto.Course2Grade);
                }
            }

            _unitOfWork.Commit();
        }

        //@PostMapping("{id}/enrollments")
        [HttpPost("{id}/enrollments")]
        public void Enroll([FromQuery] long id, [FromBody] StudentEnrollmentDto dto)
        {
            Student student = studentRepository.GetById(id);
            if (student == null)
            {
                throw new Exception("No student found with Id '{id}'");
            }

            Course course = courseRepository.GetByName(dto.GetCourse());
            if (course == null)
            {
                throw new Exception("Course is incorrect: '{dto.getCourse()}'");
            }

            bool success = Enum.IsDefined(typeof(Grade), dto.GetGrade());
            if (!success)
            {
                throw new Exception("Grade is incorrect: '{dto.getGrade()}'");
            }

            student.Enroll(course, dto.GetGrade());
            _unitOfWork.Commit();
        }

        private bool hasEnrollmentChanged(string newCourseName, string newGrade, Enrollment enrollment)
        {
            if (string.IsNullOrEmpty(newCourseName) && enrollment == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(newCourseName) || enrollment == null)
            {
                return true;
            }

            return newCourseName != enrollment.GetCourse().GetName()
                   || newGrade.ToString() != enrollment.GetGrade().ToString();
        }
    }
}