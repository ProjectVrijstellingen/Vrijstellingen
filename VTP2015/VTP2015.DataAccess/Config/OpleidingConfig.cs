using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class OpleidingConfig : EntityTypeConfiguration<Opleiding>
    {
        public OpleidingConfig()
        {
            // Primary Key
            HasKey(t => t.code);

            // Properties
            ToTable("Opleidingen");
            Property(t => t.Name).HasMaxLength(255).IsRequired();

            // Relationships

        }

    }
}
