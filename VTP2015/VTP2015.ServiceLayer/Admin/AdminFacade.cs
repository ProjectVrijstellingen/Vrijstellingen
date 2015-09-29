using System.Linq;
using System.Linq.Dynamic;
using VTP2015.DataAccess.UnitOfWork;

namespace VTP2015.ServiceLayer.Admin
{
    class AdminFacade : IAdminFacade
    {
        private readonly Repository<Entities.Counselor> _counseloRepository;

        public AdminFacade(Repository<Entities.Counselor> counseloRepository)
        {
            _counseloRepository = counseloRepository;
        }

        public void InsertCounselor(string email)
        {
            _counseloRepository.Insert(new Entities.Counselor
            {
                Email = email
            });
        }

        public void RemoveCounselor(string email)
        {
            var counselor = _counseloRepository.Table.First(c => c.Email == email);
            _counseloRepository.Delete(counselor);
        }
    }
}
