using CQRSWebApplication.Command;
using CQRSWebApplication.Repository;

namespace CQRSWebApplication.Handler;

public class EditPersonalInfoCommandHandler: ICommandHandler
{

    private UnitOfWork unitOfWork;
    public EditPersonalInfoCommandHandler(UnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public void Handle(EditPersonalInfoCommand command)
    {
        var studentRepository = new StudentRepository(unitOfWork);
        Student student = studentRepository.GetById(command.GetId());
        if (student == null)
            throw new Exception("No student found with Id '{id}'");
        student.SetName(command.GetName());
        student.SetEmail(command.GetEmail());
        unitOfWork.Commit();
    }
}