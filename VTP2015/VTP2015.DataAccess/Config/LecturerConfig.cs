using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class LecturerConfig : EntityTypeConfiguration<Lecturer>
    {
        public LecturerConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Email).HasMaxLength(255).IsRequired();
            Property(x => x.InfoMail).IsRequired();
            Property(x => x.WarningMail).IsRequired();

            // Relationships
            HasMany(t => t.PartimInformation)
                .WithRequired(d => d.Lecturer);
        }
    }
}
