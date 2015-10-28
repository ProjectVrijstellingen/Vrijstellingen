using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class PartimInformationConfig : EntityTypeConfiguration<PartimInformation>
    {
        public PartimInformationConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

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
                .WithMany(t => t.PartimInformation)
                .HasForeignKey(d => d.RouteId);



        }
    }
}
