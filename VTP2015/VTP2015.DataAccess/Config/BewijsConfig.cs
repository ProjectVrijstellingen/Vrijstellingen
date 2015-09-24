using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class BewijsConfig : EntityTypeConfiguration<Bewijs>
    {
        public BewijsConfig()
        {
            // Primary Key
            HasKey(t => t.BewijsId);

            // Properties
            ToTable("Bewijzen");
            Property(t => t.Path).IsRequired();

            // Relationships
            HasRequired(t => t.Student)
                .WithMany(t => t.Bewijzen)
                .HasForeignKey(d => d.StudentId)
                .WillCascadeOnDelete(false);

        }
    }
}