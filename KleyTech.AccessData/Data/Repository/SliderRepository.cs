﻿using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;

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
            if (dbObject is not null)
            {
                dbObject.Name = slider.Name;
                dbObject.ImageUrl = slider.ImageUrl;
                dbObject.Status = slider.Status;
            }

            //_db.SaveChanges();
        }
    }
}
