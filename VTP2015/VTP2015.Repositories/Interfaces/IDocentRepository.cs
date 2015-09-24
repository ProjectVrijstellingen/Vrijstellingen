using System;
using VTP2015.Entities;

namespace VTP2015.Repositories.Interfaces
{
    public interface IDocentRepository
    {
        Lecturer GetByEmail(string email);
        Lecturer AddDocent(string supercode);
        void ChangeInfoTime(string email, DateTime infoTime);
        void ChangeWarningTime(string email, DateTime warningTime);
        bool EmailExists(string email);
    }
}
