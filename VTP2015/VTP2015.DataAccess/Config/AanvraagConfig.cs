using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class AanvraagConfig : EntityTypeConfiguration<Request>
    {
        public AanvraagConfig()
        {
            // Primary Key
            HasKey(t => t.RequestId);

            // Properties
            ToTable("Requests");
            Property(t => t.SuperCode).HasMaxLength(255).IsRequired();
            Property(t => t.LastChanged).IsRequired();
            Property(t => t.Status).IsRequired();
            Property(t => t.Argumentation).IsRequired();


            // Relationships
            HasRequired(t => t.File)
                .WithMany(t => t.Requests)
                .HasForeignKey(d => d.FileId);

            HasRequired(t => t.PartimInformation)
                .WithMany(t => t.Requests)
                .HasForeignKey(d => d.SuperCode);

            HasMany(t => t.Evidence)
                .WithMany(t => t.Requests)
                .Map(m =>
                {
                    m.ToTable("BewijsAanvraag");
                    m.MapLeftKey("RequestId");
                    m.MapRightKey("EvidenceId");
                });
        }
    }
}