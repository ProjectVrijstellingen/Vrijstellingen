using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    public class TrajectBegeleiderConfig : EntityTypeConfiguration<Counselor>
    {
        public TrajectBegeleiderConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable("TrajectBegeleiders");
            Property(t => t.Email).HasMaxLength(255).IsRequired();

            // Relationships
            HasOptional(t => t.Education)
                .WithOptionalDependent(t => t.Counselor);

        }
    }
}
