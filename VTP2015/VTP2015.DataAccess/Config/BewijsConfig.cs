using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class BewijsConfig : EntityTypeConfiguration<Evidence>
    {
        public BewijsConfig()
        {
            // Primary Key
            HasKey(t => t.EvidenceId);

            // Properties
            ToTable("Evidence");
            Property(t => t.Path).IsRequired();

            // Relationships
            HasRequired(t => t.Student)
                .WithMany(t => t.Evidence)
                .HasForeignKey(d => d.StudentId)
                .WillCascadeOnDelete(false);

        }
    }
}