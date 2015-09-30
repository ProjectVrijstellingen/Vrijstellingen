using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace VTP2015.Identity
{
    public class IdentityManager
    {
        private readonly UserManager<ApplicationUser> _um = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private readonly RoleManager<IdentityRole> _rm = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(new ApplicationDbContext()));

        public bool RoleExists(string name)
        {
            return _rm.RoleExists(name);
        }

        public bool UserExists(string userName)
        {
            var user = _um.FindByName(userName);
            return user != null;
        }

        public bool CreateRole(string name)
        {
            var idResult = _rm.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }

        public bool CreateUser(ApplicationUser user, string password)
        {
            var idResult = _um.Create(user, password);
            return idResult.Succeeded;
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = _um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }
        public bool DeleteUserFromRole(string userId, string roleName)
        {
            var idResult = _um.RemoveFromRole(userId, roleName);
            return idResult.Succeeded;
        }

        public void ClearUserRoles(string userId)
        {
            var user = _um.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                _um.RemoveFromRole(userId, _rm.FindById(role.RoleId).Name);
            }
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            return _um.Users;
        }

        public bool HasRole(string userName, string roleName)
        {
            return true;
                //TODO: Change Role names _um.FindByName(userName).Roles.Any(userRole => userRole.RoleId == _rm.FindByName(roleName).Id);
        }
    }
}