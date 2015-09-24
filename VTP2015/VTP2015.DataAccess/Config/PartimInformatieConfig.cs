using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class PartimInformatieConfig : EntityTypeConfiguration<PartimInformatie>
    {
        public PartimInformatieConfig()
        {
            // Primary Key
            HasKey(t => t.SuperCode);

            // Properties
            ToTable("PartimInformatie");

            // Relationships
            HasRequired(t => t.Partim)
                .WithMany(t => t.PartimInformatie)
                .HasForeignKey(d => d.PartimId);
            HasRequired(t => t.Module)
                .WithMany(t => t.PartimInformatie)
                .HasForeignKey(d => d.ModuleId);
            HasRequired(t => t.Docent)
                .WithMany(t => t.PartimInformatie)
                .HasForeignKey(d => d.DocentId);



        }
    }
}
