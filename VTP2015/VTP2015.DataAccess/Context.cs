﻿using System.Data.Entity;
using VTP2015.DataAccess.Config;
using VTP2015.Entities;
using Student = VTP2015.Entities.Student;

namespace VTP2015.DataAccess
{
    public class Context : DbContext
    {
        public Context()
            : base("Name=VTP")
        {
            
        }

        public DbSet<Aanvraag> Aanvragen { get; set; }
        public DbSet<Bewijs> Bewijzen { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Dossier> Dossiers { get; set; }
        public DbSet<Student> Studenten { get; set; }
        public DbSet<TrajectBegeleider> TrajectBegeleiders { get; set; }
        public DbSet<Opleiding> Opleidingen { get; set; }
        public DbSet<Partim> Partims { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Docent> Docenten { get; set; }
        public DbSet<PartimInformatie> PartimInformatie { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AanvraagConfig());
            modelBuilder.Configurations.Add(new TrajectBegeleiderConfig());
            modelBuilder.Configurations.Add(new BewijsConfig());
            modelBuilder.Configurations.Add(new DossierConfig());
            modelBuilder.Configurations.Add(new StudentConfig());
            modelBuilder.Configurations.Add(new DocentConfig());
            modelBuilder.Configurations.Add(new OpleidingConfig());
            modelBuilder.Configurations.Add(new PartimConfig());
            modelBuilder.Configurations.Add(new ModuleConfig());
            modelBuilder.Configurations.Add(new PartimInformatieConfig());
            modelBuilder.Configurations.Add(new FeedbackConfig());
        }
    }
}
