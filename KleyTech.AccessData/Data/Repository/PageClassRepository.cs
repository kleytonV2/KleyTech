using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KleyTech.DataAccess.Data.Repository
{
    internal class PageClassRepository : Repository<PageClass>, IPageClassRepository
    {
        private readonly ApplicationDbContext _db;
        public PageClassRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PageClass pageClass)
        {
            var dbObject = _db.PageClass.FirstOrDefault(i => i.Id == pageClass.Id);
            dbObject.Name = pageClass.Name;
            dbObject.Description = pageClass.Description;
            dbObject.Status = pageClass.Status;
            //_db.SaveChanges();
        }
    }
}
