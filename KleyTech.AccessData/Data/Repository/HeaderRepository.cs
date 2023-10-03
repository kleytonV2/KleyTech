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
    internal class HeaderRepository : Repository<Header>, IHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public HeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Header header)
        {
            var dbObject = _db.Headers.FirstOrDefault(i => i.Id == header.Id);
            dbObject.Name = header.Name;
            dbObject.LogoURL = header.LogoURL;
            dbObject.HTML_Id = header.HTML_Id;
            dbObject.HTML_Class = header.HTML_Class;

            //_db.SaveChanges();
        }
    }
}
