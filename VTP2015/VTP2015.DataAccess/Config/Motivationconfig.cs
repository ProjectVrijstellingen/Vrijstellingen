using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class MotivationConfig : EntityTypeConfiguration<Motivation>
    {
        public MotivationConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable("Motivations");
            Property(t => t.Text).IsRequired();

            // Relationships
        }
    }
}
