using System.Linq;
using VTP2015.DataAccess.UnitOfWork;

namespace VTP2015.ServiceLayer.Feedback
{
    public class FeedbackFacade : IFeedbackFacade
    {
        private readonly Repository<Entities.Student> _studentRepository;
        private readonly Repository<Entities.Feedback> _feedbackRepository;

        public FeedbackFacade(Repository<Entities.Feedback> feedbackRepository, Repository<Entities.Student> studentRepository)
        {
            _feedbackRepository = feedbackRepository;
            _studentRepository = studentRepository;
        }

        public Entities.Student GetStudentByEmail(string email)
        {
            return _studentRepository.Table.First(s => s.Email == email);
        }

        public void InsertFeedback(Entities.Feedback feedback)
        {
            _feedbackRepository.Insert(feedback);
        }
    }
}