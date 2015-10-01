﻿using System.Collections.Generic;
using System.Linq;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Counselor
{
    public class CounselorFacade : ICounselorFacade
    {
        private readonly Repository<Request> _requestRepository;
        private readonly Repository<Education> _educationRepository;
        private readonly Repository<Entities.Counselor> _counselorRepository;
        private readonly Repository<File> _fileRepository;

        public CounselorFacade(IUnitOfWork unitOfWork)
        {
            _requestRepository = unitOfWork.Repository<Request>();
            _educationRepository = unitOfWork.Repository<Education>();
            _counselorRepository = unitOfWork.Repository<Entities.Counselor>();
            _fileRepository = unitOfWork.Repository<File>();
        }

        public IQueryable<Request> GetRequests()
        {
            return _requestRepository.Table;
        }

        public string GetEducationNameByCounselorEmail(string email)
        {
            if (!_counselorRepository.Table.Any(x => x.Email == email)) return "";
            return _counselorRepository.Table.First(s => s.Email == email)
                .Education.Name;
        }

        public IQueryable<Education> GetEducations()
        {
            return _educationRepository.Table;
        }

        public void ChangeEducation(string email, string educationName)
        {
            var education = _educationRepository.Table.First(e => e.Name == educationName);
            var counselor = _counselorRepository.Table.First(c => c.Email == email);

            counselor.Education = education;
            _counselorRepository.Update(counselor);
        }

        public IQueryable<File> GetFileByCounselorEmail(string email, string academicYear)
        {
            if (!_counselorRepository.Table.Any()) return new List<File>().AsQueryable();
            var educationId = _counselorRepository.Table.First(t => t.Email == email).Education.Id;
            return _fileRepository.Table.Where(
                d => d.Requests.Count > 0 && d.AcademicYear == academicYear && d.Student.Education.Id == educationId);
        }

        public void SendReminder(int aanvraagId)
        {
            throw new System.NotImplementedException();//todo
        }
    }
}