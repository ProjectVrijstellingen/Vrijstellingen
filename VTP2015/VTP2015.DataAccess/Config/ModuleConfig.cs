using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class ModuleConfig : EntityTypeConfiguration<Module>
    {
        public ModuleConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable("Modules");
            Property(t => t.Name).HasMaxLength(255).IsRequired();

            // Relationships
        }
    }
}
