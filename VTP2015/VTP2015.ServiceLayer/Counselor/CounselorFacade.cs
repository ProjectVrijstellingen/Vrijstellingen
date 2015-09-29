using System.Linq;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Counselor
{
    public class CounselorFacade : ICounselorFacade
    {
        private readonly Repository<Request> _requestRepository;
        private readonly Repository<Entities.Student> _studentRepository;
        private readonly Repository<Education> _educationRepository;
        private readonly Repository<Entities.Counselor> _counseloRepository;
        private readonly Repository<File> _fileRepository;

        public CounselorFacade(Repository<Request> requestRepository, Repository<Entities.Student> studentRepository,
            Repository<Education> educationRepository, Repository<Entities.Counselor> counseloRepository,
            Repository<File> fileRepository)
        {
            _requestRepository = requestRepository;
            _studentRepository = studentRepository;
            _educationRepository = educationRepository;
            _counseloRepository = counseloRepository;
            _fileRepository = fileRepository;
        }

        public IQueryable<Request> GetRequests()
        {
            return _requestRepository.Table;
        }

        public string GetEducationNameByStudentEmail(string email)
        {
            return _studentRepository.Table.First(s => s.Email == email)
                .Education.Name;
        }

        public IQueryable<Education> GetEducations()
        {
            return _educationRepository.Table;
        }

        public void ChangeEducation(string email, string educationName)
        {
            var education = _educationRepository.Table.First(e => e.Name == educationName);
            var counselor = _counseloRepository.Table.First(c => c.Email == email);

            counselor.Education = education;
            _counseloRepository.Update(counselor);
        }

        public IQueryable<File> GetFileByCounselorEmail(string email, string academicYear)
        {
            var educationId = _counseloRepository.Table.First(t => t.Email == email).Education.Id;
            return _fileRepository.Table.Where(
                d => d.Requests.Count > 0 && d.AcademicYear == academicYear && d.Student.EducationId == educationId);
        }

        public void SendReminder(int aanvraagId)
        {
            throw new System.NotImplementedException();
        }
    }
}