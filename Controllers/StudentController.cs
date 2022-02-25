using CQRSWebApplication.Command;
using CQRSWebApplication.Dto;
using CQRSWebApplication.Handler;
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

        //@PutMapping("{id}/enrollments/{enrollmentNumber}")
        [HttpPost("{id}/enrollments/{enrollmentNumber}")]
        public void Transfer([FromRoute] long id, [FromRoute] int enrollmentNumber, [FromBody] StudentTransferDto dto)
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

            Enrollment enrollment = student.GetEnrollment(enrollmentNumber);
            if (!success)
            {
                throw new Exception("No enrollment found with number '{enrollmentNumber}'");
            }

            enrollment.Update(course, dto.GetGrade());
            _unitOfWork.Commit();
        }

        //@PostMapping("{id}/enrollments/{enrollmentNumber}/deletion")
        [HttpPost("{id}/enrollments/{enrollmentNumber}/deletion")]
        public void DisEnroll([FromRoute] long id, [FromRoute] int enrollmentNumber,
            [FromBody] StudentDisEnrollmentDto dto)
        {
            Student student = studentRepository.GetById(id);
            if (student == null)
            {
                throw new Exception("No enrollment found with number '{enrollmentNumber}'");
            }

            if (string.IsNullOrEmpty(dto.GetComment()))
            {
                throw new Exception("Disenrollment comment is required");
            }

            Enrollment enrollment = student.GetEnrollment(enrollmentNumber);

            if (enrollment == null)
            {
                throw new Exception("No enrollment found with number '{enrollmentNumber}'");
            }

            student.RemoveEnrollment(enrollment, dto.GetComment());
            student.AddDisenrollmentComment(enrollment, dto.GetComment());
            _unitOfWork.Commit();
        }

        //@PutMapping("{id}")
        [HttpPost("{id}")]
        public void EditPersonalInfo( [FromBody] EditPersonalInfoCommand command)
        {
            var handler = new EditPersonalInfoCommandHandler(_unitOfWork);
            handler.Handle(command);
            //return result.isSuccess ? Ok() : NotFound();
        }
    }
}