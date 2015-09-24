using VTP2015.Entities;

namespace VTP2015.ServiceLayer
{
    public interface IFeedbackFacade
    {
        Student GetStudentByEmail(string email);
        void InsertFeedback(Feedback feedback);
    }
}
