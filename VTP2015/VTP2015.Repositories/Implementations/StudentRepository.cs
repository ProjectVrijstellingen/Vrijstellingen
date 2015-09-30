using System.Linq;
using VTP2015.DataAccess;
using VTP2015.DataAccess.Identity;
using VTP2015.Entities;
using VTP2015.Repositories.Interfaces;
using VTP2015.Repositories.Remote_Services;

namespace VTP2015.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDataAccessFacade _db;
        private readonly IGenericRepository<Student> _genericRepository;
        private readonly IBamaflexRepository _bamaflexRepository;
        private readonly IOpleidingRepository _opleidingRepository;

        public StudentRepository(IDataAccessFacade db, IBamaflexRepository bamaflexRepository, IOpleidingRepository opleidingRepository)
        {
            _bamaflexRepository = bamaflexRepository;
            _opleidingRepository = opleidingRepository;
            _db = db;
            _genericRepository = new GenericRepository<Student>(db.Context);
        }

        public Student GetByEmail(string email)
        {
            return _genericRepository.AsQueryable(s => s.Email == email).First();
        }

        public void SyncStudentByUser(User user)
        {
            var opleidingNaam = _bamaflexRepository.GetOpleidingByStudentId(user.Id);

            Student student;
            if (_db.Context.Studenten.Any(x => x.StudentId == user.Id))
            {
                student = _db.Context.Studenten.First(x => x.StudentId == user.Id);
                student.Name = user.Lastname;
                student.FirstName = user.Firstname;
                student.Email = user.Email;
                student.PhoneNumber = user.ExtraInfo1;
                student.Opleiding = _opleidingRepository.GetOpleidingen().First(x => x.Naam == opleidingNaam);
            }
            else
            {
                student = new Student
                {
                    StudentId = user.Id,
                    Name = user.Lastname,
                    FirstName = user.Firstname,
                    Email = user.Email,
                    PhoneNumber = user.ExtraInfo1,
                    Opleiding = _opleidingRepository.GetOpleidingen().First(x => x.Name == opleidingNaam)
                };
                _db.Context.Studenten.Add(student);
            }
            _db.Context.SaveChanges();
        }

        public string GetStudentIdByEmail(string email)
        {
            return _genericRepository.AsQueryable(user => user.Email == email).First().StudentId;
        }


        public bool IsAanvraagFromStudent(int dossierId, string supercode, string email)
        {
            return _genericRepository.AsQueryable(s => s.Email == email).Select(s => s.Opleiding).SelectMany(x => x.KeuzeTrajecten)/*needs fix keuzetraject added from student*/.SelectMany(x => x.PartimInformatie).Any(p => p.SuperCode == supercode) && _genericRepository.AsQueryable(s => s.Email == email).SelectMany(s => s.Dossiers).Any(d => d.DossierId == dossierId);
        }
    }
}