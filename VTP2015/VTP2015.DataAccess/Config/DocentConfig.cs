using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTP2015.Entities;

namespace VTP2015.DataAccess.Config
{
    class DocentConfig : EntityTypeConfiguration<Lecturer>
    {
        public DocentConfig()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            ToTable("Lecturers");
            Property(t => t.Email).HasMaxLength(255).IsRequired();
            Property(x => x.InfoMail).IsRequired();
            Property(x => x.WarningMail).IsRequired();

            // Relationships
            HasMany(t => t.PartimInformation)
                .WithRequired(d => d.Lecturer);
        }
    }
}
