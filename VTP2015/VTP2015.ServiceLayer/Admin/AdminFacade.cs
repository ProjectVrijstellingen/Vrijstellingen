using System.Linq;
using VTP2015.DataAccess.UnitOfWork;

namespace VTP2015.ServiceLayer.Admin
{
    public class AdminFacade : IAdminFacade
    {
        private readonly IRepository<Entities.Counselor> _counselorRepository;

        public AdminFacade(IUnitOfWork unitOfWork)
        {
            _counselorRepository = unitOfWork.Repository<Entities.Counselor>();
        }

        public void InsertCounselor(string email)
        {
            _counselorRepository.Insert(new Entities.Counselor
            {
                Email = email
            });
        }

        public void RemoveCounselor(string email)
        {
            var counselor = _counselorRepository.Table.First(c => c.Email == email);
            _counselorRepository.Delete(counselor);
        }
    }
}
