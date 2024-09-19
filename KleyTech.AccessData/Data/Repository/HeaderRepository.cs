using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;

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
            if (dbObject is not null)
            {
                dbObject.Name = header.Name;
                dbObject.LogoURL = header.LogoURL;
                dbObject.HTML_Id = header.HTML_Id;
            }

            //_db.SaveChanges();
        }
    }
}
