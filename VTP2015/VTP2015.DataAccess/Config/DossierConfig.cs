using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class DossierConfig : EntityTypeConfiguration<Dossier>
    {
        public DossierConfig()
        {
            // Primary Key
            HasKey(t => t.DossierId);

            // Properties
            ToTable("Dossiers");
            Property(t => t.AanmaakDatum).IsRequired();
            Property(t => t.AcademieJaar).HasMaxLength(8).IsRequired();
            Property(t => t.Editable).IsRequired();

            // Relationships
            HasRequired(t => t.Student)
                .WithMany(t => t.Dossiers)
                .HasForeignKey(d => d.StudentId);


        }
    }
}