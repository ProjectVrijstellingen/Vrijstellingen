using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    public class KeuzeTrajectConfig:EntityTypeConfiguration<KeuzeTraject>
    {
        public KeuzeTrajectConfig()
        {
            ToTable("KeuzeTraject");
            Property(t => t.Name).HasMaxLength(255).IsRequired();
        }
    }
}
