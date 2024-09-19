using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;

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
            if (dbObject is not null)
            { 
                dbObject.Name = pageClass.Name;
                dbObject.Description = pageClass.Description;
                dbObject.Status = pageClass.Status;
                //_db.SaveChanges();    
            }
        }
    }
}
