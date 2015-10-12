using VTP2015.DataAccess;
using System.Data.Entity.Migrations;

namespace VTP2015.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VTP2015.Identity;
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
        }

        bool AddRoles()
        {
            var im = new IdentityManager();
            bool success = false;
            success = im.CreateRole("Students");
            if (!success) return success;
            success = im.CreateRole("Lecturers");
            if (!success) return success;
            success = im.CreateRole("Counselors");
            if (!success) return success;
            success = im.CreateRole("Admins");
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
            success = im.AddUserToRole(newUser.Id, "Counselors");
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
