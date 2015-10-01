using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class PartimConfig : EntityTypeConfiguration<Partim>
    {
        public PartimConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable("Partims");
            Property(t => t.Name).HasMaxLength(255).IsRequired();

            // Relationships
        }
    }
}
