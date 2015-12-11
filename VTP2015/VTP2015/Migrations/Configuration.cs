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
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Context context)
        {
            //this.AddRoles();
            //this.SetMotivationsDB();
        }

        private void SetMotivationsDB()
        {
            var database = new Context();
            database.Motivations.Add(new Motivation {Id = 0, Text = "geen"});
            database.Motivations.Add(new Motivation { Id = 1, Text = "diploma vorige opleiding" });
            database.Motivations.Add(new Motivation { Id = 2, Text = "studieomvang en inhoud stemmen overeen" });
            database.Motivations.Add(new Motivation { Id = 3, Text = "studieomvang en inhoud stemmen niet overeen" });
            database.Motivations.Add(new Motivation { Id = 4, Text = "studieomvang onvoldoende" });
            database.Motivations.Add(new Motivation { Id = 5, Text = "inhoud stemt niet voldoende overeen" });
            database.Motivations.Add(new Motivation { Id = 6, Text = "geen creditbewijs behaald" });
            database.Motivations.Add(new Motivation { Id = 7, Text = "creditbewijs > 5 jaar" });
            database.SaveChanges();
        }

        bool AddRoles()
        {
            var im = new IdentityManager();
            bool success = false;
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
