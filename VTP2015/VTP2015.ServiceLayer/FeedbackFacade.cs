using System.Linq;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer
{
    class FeedbackFacade : IFeedbackFacade
    {
        private readonly Repository<Student> _studentRepository;
        private readonly Repository<Feedback> _feedbackRepository;

        public FeedbackFacade(Repository<Feedback> feedbackRepository, Repository<Student> studentRepository)
        {
            _feedbackRepository = feedbackRepository;
            _studentRepository = studentRepository;
        }

        public Student GetStudentByEmail(string email)
        {
            return _studentRepository.Table.First(s => s.Email == email);
        }

        public void InsertFeedback(Feedback feedback)
        {
            _feedbackRepository.Insert(feedback);
        }
    }
}