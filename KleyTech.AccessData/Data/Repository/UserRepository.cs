using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleyTech.DataAccess.Data.Repository
{
    internal class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void LockUser(string id)
        {
            var user = _db.ApplicationUser.FirstOrDefault(u => u.Id == id);
            user.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void UnlockUser(string id)
        {
            var user = _db.ApplicationUser.FirstOrDefault(u => u.Id == id);
            user.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }

    }
}
