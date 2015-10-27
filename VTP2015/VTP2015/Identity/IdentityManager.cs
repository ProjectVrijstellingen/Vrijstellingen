using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace VTP2015.Identity
{
    public class IdentityManager
    {
        private readonly UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private readonly RoleManager<IdentityRole> _roleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(new ApplicationDbContext()));

        public bool RoleExists(string name)
        {
            return _roleManager.RoleExists(name);
        }

        public bool UserExists(string userName)
        {
            var user = _userManager.FindByName(userName);
            return user != null;
        }

        public bool CreateRole(string name)
        {
            var idResult = _roleManager.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }

        public bool CreateUser(ApplicationUser user, string password)
        {
            var idResult = _userManager.Create(user, password);
            return idResult.Succeeded;
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }
        public bool DeleteUserFromRole(string userId, string roleName)
        {
            var idResult = _userManager.RemoveFromRole(userId, roleName);
            return idResult.Succeeded;
        }

        public void ClearUserRoles(string userId)
        {
            var user = _userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                _userManager.RemoveFromRole(userId, _roleManager.FindById(role.RoleId).Name);
            }
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            return _userManager.Users;
        }

        public bool HasRole(string userName, string roleName)
        {
            return _userManager.FindByName(userName).Roles.Any(userRole => userRole.RoleId == _roleManager.FindByName(roleName).Id);
        }
    }
}