using System.Linq;
using VTP2015.DataAccess.UnitOfWork;

namespace VTP2015.ServiceLayer.Feedback
{
    public class FeedbackFacade : IFeedbackFacade
    {
        private readonly IRepository<Entities.Student> _studentRepository;
        private readonly IRepository<Entities.Feedback> _feedbackRepository;

        public FeedbackFacade(IUnitOfWork unitOfWork)
        {
            _feedbackRepository = unitOfWork.Repository<Entities.Feedback>();
            _studentRepository = unitOfWork.Repository<Entities.Student>();
        }

        public void InsertFeedback(Models.Feedback feedback)
        {
            var entity = new Entities.Feedback()
            {
                Student = _studentRepository.Table.First(s => s.Email == feedback.StudentEmail),
                Text = feedback.Text
            };

            _feedbackRepository.Insert(entity);
        }
    }
}