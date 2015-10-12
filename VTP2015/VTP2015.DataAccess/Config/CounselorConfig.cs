using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    public class CounselorConfig : EntityTypeConfiguration<Counselor>
    {
        public CounselorConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Email).HasMaxLength(255).IsRequired();

            // Relationships
            HasOptional(t => t.Education)
                .WithMany(t => t.Counselors)
                .HasForeignKey(t => t.EducationId);
        }
    }
}
