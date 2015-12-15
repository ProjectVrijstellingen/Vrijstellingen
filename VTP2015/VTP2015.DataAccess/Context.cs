using System.Data.Entity;
using VTP2015.DataAccess.Config;
using VTP2015.Entities;
using Student = VTP2015.Entities.Student;

namespace VTP2015.DataAccess
{
    public class Context : DbContext
    {
        public Context()
            : base("Name=VTP")
        {
            
        }

        public DbSet<Request> Request { get; set; }
        public DbSet<Evidence> Evidence { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Counselor> Counselors { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Partim> Partims { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<PartimInformation> PartimInformation { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Motivation> Motivations { get; set; } 
        public DbSet<RequestPartimInformation> RequestPartimInformations { get; set; }
        public DbSet<PrevEducation> PrevEducations { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RequestConfig());
            modelBuilder.Configurations.Add(new CounselorConfig());
            modelBuilder.Configurations.Add(new EvidenceConfig());
            modelBuilder.Configurations.Add(new FileConfig());
            modelBuilder.Configurations.Add(new StudentConfig());
            modelBuilder.Configurations.Add(new LecturerConfig());
            modelBuilder.Configurations.Add(new EducationConfig());
            modelBuilder.Configurations.Add(new PartimConfig());
            modelBuilder.Configurations.Add(new ModuleConfig());
            modelBuilder.Configurations.Add(new PartimInformationConfig());
            modelBuilder.Configurations.Add(new FeedbackConfig());
            modelBuilder.Configurations.Add(new RouteConfig());
            modelBuilder.Configurations.Add(new MotivationConfig());
            modelBuilder.Configurations.Add(new RequestPartimInformationConfig());
            modelBuilder.Configurations.Add(new PrevEducationConfig());
        }
    }
}
