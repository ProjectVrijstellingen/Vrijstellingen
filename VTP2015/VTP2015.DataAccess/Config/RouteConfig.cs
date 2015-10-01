using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    public class RouteConfig:EntityTypeConfiguration<Route>
    {
        public RouteConfig()
        {
            ToTable("Route");
            Property(t => t.Name).HasMaxLength(255).IsRequired();
        }
    }
}
