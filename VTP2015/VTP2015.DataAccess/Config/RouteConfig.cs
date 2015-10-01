using System.Data.Entity.ModelConfiguration;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    public class RouteConfig:EntityTypeConfiguration<Route>
    {
        public RouteConfig()
        {
            ToTable("Route");
            Property(t => t.Name).HasMaxLength(255).IsRequired();

            // Relationships
            HasRequired(t => t.Education)
                .WithMany(t => t.Routes)
                .HasForeignKey(t => t.EducationId);
        }
    }
}
