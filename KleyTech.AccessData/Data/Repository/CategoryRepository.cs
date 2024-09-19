using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KleyTech.DataAccess.Data.Repository
{
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCategoryList()
        {
            return _db.Categories.Select(i => new SelectListItem() { 
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var dbObject = _db.Categories.FirstOrDefault(i => i.Id == category.Id);
            if (dbObject is not null)
            {
                dbObject.Name = category.Name;
                dbObject.Order = category.Order;

                _db.SaveChanges();
            }
        }
    }
}
