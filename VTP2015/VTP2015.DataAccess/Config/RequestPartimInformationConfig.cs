using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class RequestPartimInformationConfig : EntityTypeConfiguration<RequestPartimInformation>
    {
        public RequestPartimInformationConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable("RequestPartimInformations");
            Property(t => t.Status).IsRequired();

            // Relationships
            HasRequired(r => r.Request)
                .WithMany(r => r.RequestPartimInformations)
                .HasForeignKey(r => r.RequestId);
            
            HasRequired(r => r.PartimInformation)
                .WithMany(p => p.RequestPartimInformations)
                .HasForeignKey(r => r.PartimInformationId);

            HasRequired(r => r.Motivation)
                .WithMany(r => r.RequestPartimInformations)
                .HasForeignKey(r => r.MotivationId);
        }
    }
}
