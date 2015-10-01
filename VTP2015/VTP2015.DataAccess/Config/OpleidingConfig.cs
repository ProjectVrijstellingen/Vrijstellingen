using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class OpleidingConfig : EntityTypeConfiguration<Education>
    {
        public OpleidingConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable("Educations");
            Property(t => t.Name).HasMaxLength(255).IsRequired();

            // Relationships

        }

    }
}
