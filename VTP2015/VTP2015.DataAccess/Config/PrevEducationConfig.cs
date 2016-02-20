using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class PrevEducationConfig : EntityTypeConfiguration<PrevEducation>
    {
        public PrevEducationConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable("PrevEducations");
            Property(t => t.Education).IsRequired();

            // Relationships
            HasRequired(t => t.Student)
                .WithMany(t => t.PrevEducations)
                .HasForeignKey(d => d.StudentId)
                .WillCascadeOnDelete(false);

        }
    }
}