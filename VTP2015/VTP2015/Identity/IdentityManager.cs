using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace VTP2015.Identity
{
    public class IdentityManager
    {
        private UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private RoleManager<IdentityRole> rm = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(new ApplicationDbContext()));

        public bool RoleExists(string name)
        {
            return rm.RoleExists(name);
        }

        public bool UserExists(string userName)
        {
            var user = um.FindByName(userName);
            return user != null;
        }

        public bool CreateRole(string name)
        {
            var idResult = rm.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }

        public bool CreateUser(ApplicationUser user, string password)
        {
            var idResult = um.Create(user, password);
            return idResult.Succeeded;
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }
        public bool DeleteUserFromRole(string userId, string roleName)
        {
            var idResult = um.RemoveFromRole(userId, roleName);
            return idResult.Succeeded;
        }

        public void ClearUserRoles(string userId)
        {
            var user = um.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                um.RemoveFromRole(userId, rm.FindById(role.RoleId).Name);
            }
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            return um.Users;
        }

        public bool HasRole(string userName, string roleName)
        {
            return um.FindByName(userName).Roles.Any(userRole => userRole.RoleId == rm.FindByName(roleName).Id);
        }
    }
}