using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class StudentConfig : EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable("Students");
            Property(t => t.Name).HasMaxLength(30).IsRequired();
            Property(t => t.FirstName).HasMaxLength(30).IsRequired();
            Property(t => t.Email).HasMaxLength(255).IsRequired();
            Property(t => t.PhoneNumber).IsRequired();

            // Relationships

            HasRequired(t => t.Education)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.Id);

            HasMany(t => t.PartimInformation)
                .WithMany(t => t.Students)
                .Map(m =>
                {
                    m.ToTable("StudentPartimInformation");
                    m.MapLeftKey("StudentId");
                    m.MapRightKey("SuperCode");
                });
        }
    }
}