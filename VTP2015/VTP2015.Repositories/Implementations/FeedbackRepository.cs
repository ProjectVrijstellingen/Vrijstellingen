using VTP2015.DataAccess;
using VTP2015.Entities;
using VTP2015.Repositories.Interfaces;
using VTP2015.Repositories.Remote_Services;

namespace VTP2015.Repositories.Implementations
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IDataAccessFacade _db;
        private readonly GenericRepository<Feedback> _genericRepository;

        public FeedbackRepository(IDataAccessFacade db)
        {
            _db = db;
            _genericRepository = new GenericRepository<Feedback>(db.Context);
        }

        public void AddFeedback(Feedback feedback)
        {
            _genericRepository.Insert(feedback);
        }
    }
}
