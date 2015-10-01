using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class PartimInformatieConfig : EntityTypeConfiguration<PartimInformation>
    {
        public PartimInformatieConfig()
        {
            // Primary Key
            HasKey(t => t.SuperCode);

            // Properties
            ToTable("PartimInformation");

            // Relationships
            HasRequired(t => t.Partim)
                .WithMany(t => t.PartimInformation)
                .HasForeignKey(d => d.PartimId);
            HasRequired(t => t.Module)
                .WithMany(t => t.PartimInformation)
                .HasForeignKey(d => d.ModuleId);
            HasRequired(t => t.Lecturer)
                .WithMany(t => t.PartimInformation)
                .HasForeignKey(d => d.LecturerId);
            HasOptional(t => t.Route)
                .WithMany(t => t.PartimInformatie)
                .HasForeignKey(d => d.RouteId);



        }
    }
}
