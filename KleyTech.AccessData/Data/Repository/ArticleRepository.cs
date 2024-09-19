﻿using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;

namespace KleyTech.DataAccess.Data.Repository
{
    internal class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext _db;
        public ArticleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Article article)
        {
            var dbObject = _db.Articles.FirstOrDefault(i => i.Id == article.Id);
            if (dbObject is not null)
            {
                dbObject.Name = article.Name;
                dbObject.Description = article.Description;
                dbObject.ImageURL = article.ImageURL;
                dbObject.CategoryId = article.CategoryId;

                //_db.SaveChanges();
            }
        }
    }
}
