using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Synchronisation
{
    interface IBamaflexSynchroniser
    {
        bool SyncStudentPartims(string email, string academicYear);
        Education SyncEducations(string educationCode, string academicYear);
        void SyncStudentByUser(string email, string academicYear);
    }
}
