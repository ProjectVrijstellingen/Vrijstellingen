using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class EducationConfig : EntityTypeConfiguration<Education>
    {
        public EducationConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name).HasMaxLength(255).IsRequired();
            Property(t => t.AcademicYear).HasMaxLength(255).IsRequired();
            Property(t => t.Code).HasMaxLength(255).IsRequired();

            // Relationships
            
        }

    }
}
