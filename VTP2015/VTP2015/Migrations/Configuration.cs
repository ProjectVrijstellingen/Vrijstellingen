using VTP2015.DataAccess;
using System.Data.Entity.Migrations;
using System.Linq;
using VTP2015.Entities;
using VTP2015.Identity;

namespace VTP2015.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Context context)
        {
            this.AddRoles();
            this.SetMotivationsDb();
            //this.UpdateChanges();
        }

        //function used to make db changes after update
        private void UpdateChanges()
        {
            var database = new Context();
            var motivation = database.Motivations.First(x => x.Text == "geen");
            foreach (var request in database.RequestPartimInformations.Where(x => x.Motivation == null))
            {
                request.Motivation = motivation;
            }
            database.SaveChanges();
        }

        private void SetMotivationsDb()
        {
            var database = new Context();
            if (database.Motivations.Any(x => x.Id == 1 && x.Text == "geen")) return;
            database.Motivations.RemoveRange(database.Motivations);
            database.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Motivations', RESEED, 0)");
            database.Motivations.Add(new Motivation {Text = "geen"});
            database.Motivations.Add(new Motivation {Text = "diploma vorige opleiding" });
            database.Motivations.Add(new Motivation {Text = "studieomvang en inhoud stemmen overeen" });
            database.Motivations.Add(new Motivation {Text = "studieomvang en inhoud stemmen niet overeen" });
            database.Motivations.Add(new Motivation {Text = "studieomvang onvoldoende" });
            database.Motivations.Add(new Motivation {Text = "inhoud stemt niet voldoende overeen" });
            database.Motivations.Add(new Motivation {Text = "geen creditbewijs behaald" });
            database.Motivations.Add(new Motivation {Text = "creditbewijs > 5 jaar" });
            database.SaveChanges();
        }

        bool AddRoles()
        {
            var im = new IdentityManager();
            if (im.UserExists("begeleider@howest.be")) return true;
            var success = false;
            success = im.CreateRole("Student");
            if (!success) return success;
            success = im.CreateRole("Lecturer");
            if (!success) return success;
            success = im.CreateRole("Counselor");
            if (!success) return success;
            success = im.CreateRole("Admin");
            if (!success) return success;

            var newUser = new ApplicationUser()
            {
                UserName = "begeleider@howest.be",
                Email = "begeleider@howest.be"
            };
            success = im.CreateUser(newUser, "begeleider");
            if (!success) return success;
            success = im.AddUserToRole(newUser.Id, "Admin");
            if (!success) return success;
            success = im.AddUserToRole(newUser.Id, "Counselor");
            if (!success) return success;
            success = im.AddUserToRole(newUser.Id, "Lecturer");
            if (!success) return success;

            newUser = new ApplicationUser()
            {
                UserName = "docent@howest.be",
                Email = "docent@howest.be"
            };
            success = im.CreateUser(newUser, "docent");
            if (!success) return success;
            success = im.AddUserToRole(newUser.Id, "Lecturer");
            if (!success) return success;

            newUser = new ApplicationUser()
            {
                UserName = "student@student.howest.be",
                Email = "student@student.howest.be"
            };
            success = im.CreateUser(newUser, "student");
            if (!success) return success;
            success = im.AddUserToRole(newUser.Id, "Student");
            if (!success) return success;

            return true;
        }
    }
}
