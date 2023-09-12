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
    internal class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;
        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Slider slider)
        {
            var dbObject = _db.Sliders.FirstOrDefault(i => i.Id == slider.Id);
            dbObject.Name = slider.Name;
            dbObject.ImageUrl = slider.ImageUrl;
            dbObject.Status = slider.Status;

            //_db.SaveChanges();
        }
    }
}
