namespace VTP2015.ServiceLayer.Feedback
{
    public interface IFeedbackFacade
    {
        Entities.Student GetStudentByEmail(string email);
        void InsertFeedback(Entities.Feedback feedback);
    }
}
