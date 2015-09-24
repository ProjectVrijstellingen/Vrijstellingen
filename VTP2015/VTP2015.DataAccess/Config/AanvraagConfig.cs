using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class AanvraagConfig : EntityTypeConfiguration<Aanvraag>
    {
        public AanvraagConfig()
        {
            // Primary Key
            HasKey(t => t.AanvraagId);

            // Properties
            ToTable("Aanvragen");
            Property(t => t.SuperCode).HasMaxLength(255).IsRequired();
            Property(t => t.LastChanged).IsRequired();
            Property(t => t.Status).IsRequired();
            Property(t => t.Argumentatie).IsRequired();


            // Relationships
            HasRequired(t => t.Dossier)
                .WithMany(t => t.Aanvragen)
                .HasForeignKey(d => d.DossierId);

            HasRequired(t => t.PartimInformatie)
                .WithMany(t => t.Aanvragen)
                .HasForeignKey(d => d.SuperCode);

            HasMany(t => t.Bewijzen)
                .WithMany(t => t.Aanvragen)
                .Map(m =>
                {
                    m.ToTable("BewijsAanvraag");
                    m.MapLeftKey("AanvraagId");
                    m.MapRightKey("BewijsId");
                });
        }
    }
}