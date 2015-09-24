using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Interfaces
{
    public interface IFeedbackFacade
    {
        Student GetStudentByEmail(string email);
        void InsertFeedback(Feedback feedback);
    }
}
