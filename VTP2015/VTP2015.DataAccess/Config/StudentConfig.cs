using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class StudentConfig : EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {
            // Primary Key
            HasKey(t => t.StudentId);

            // Properties
            ToTable("Studenten");
            Property(t => t.Naam).HasMaxLength(30).IsRequired();
            Property(t => t.VoorNaam).HasMaxLength(30).IsRequired();
            Property(t => t.Email).HasMaxLength(255).IsRequired();
            Property(t => t.PhoneNumber).IsRequired();

            // Relationships

            HasRequired(t => t.Opleiding)
                .WithMany(t => t.Studenten)
                .HasForeignKey(d => d.OpleidingId);

            HasMany(t => t.PartimInformatie)
                .WithMany(t => t.Studenten)
                .Map(m =>
                {
                    m.ToTable("StudentPartimInformatie");
                    m.MapLeftKey("StudentId");
                    m.MapRightKey("SuperCode");
                });
        }
    }
}