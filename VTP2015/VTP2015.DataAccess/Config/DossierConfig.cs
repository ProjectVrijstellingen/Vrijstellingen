using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class DossierConfig : EntityTypeConfiguration<File>
    {
        public DossierConfig()
        {
            // Primary Key
            HasKey(t => t.FileId);

            // Properties
            ToTable("Files");
            Property(t => t.AanmaakDatum).IsRequired();
            Property(t => t.AcademicYear).HasMaxLength(8).IsRequired();
            Property(t => t.Editable).IsRequired();

            // Relationships
            HasRequired(t => t.Student)
                .WithMany(t => t.Files)
                .HasForeignKey(d => d.StudentId);


        }
    }
}