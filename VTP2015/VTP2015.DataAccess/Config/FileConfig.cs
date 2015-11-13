using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class FileConfig : EntityTypeConfiguration<File>
    {
        public FileConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable("Files");
            Property(t => t.DateCreated).IsRequired();
            Property(t => t.AcademicYear).HasMaxLength(8).IsRequired();
            Property(t => t.FileStatus).IsRequired();

            // Relationships
            HasRequired(t => t.Student)
                .WithMany(t => t.Files)
                .HasForeignKey(d => d.StudentId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Education)   
                .WithMany(t => t.Files)
                .HasForeignKey(d => d.EducationId);
        }
    }
}