using KleyTech.Data;
using KleyTech.DataAccess.Initializer;
using KleyTech.Models;
using KleyTech.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleyTech.DataAccess
{
    public class InitializerDB : IInitializerDB
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InitializerDB(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0) { 
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            //Creating roles
            if (_db.Roles.Any(ro => ro.Name == CNT.Admin)) return;
            _roleManager.CreateAsync(new IdentityRole(CNT.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(CNT.User)).GetAwaiter().GetResult();

            //Creating users
            _userManager.CreateAsync(new ApplicationUser { 
                UserName = "kleyton@hotmail.es",
                Email = "kleyton@hotmail.es",
                EmailConfirmed = true,
                Name = "Admin"
            },"Kleyton200.").GetAwaiter().GetResult();

            //Creating user - roles rel
            ApplicationUser user = _db.ApplicationUser
                .Where(u => u.Email == "kleyton@hotmail.es")
                .FirstOrDefault();
            _userManager.AddToRoleAsync(user,CNT.Admin).GetAwaiter().GetResult();

        }
    }
}
