using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleyTech.DataAccess.Data.Repository
{
    public class WorkContainer : IWorkContainer
    {
        private readonly ApplicationDbContext _db;

        public WorkContainer(ApplicationDbContext db)
        {
            _db = db;
            PageClass = new PageClassRepository(_db);
            Block = new BlockRepository(_db);
            Header = new HeaderRepository(_db);
            Category = new CategoryRepository(_db);
            Article = new ArticleRepository(_db);
            Slider = new SliderRepository(_db);
            User = new UserRepository(_db);
        }

        public IHeaderRepository Header { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IArticleRepository Article { get; private set; }
        public ISliderRepository Slider { get; private set; }
        public IUserRepository User { get; private set; }
        public IPageClassRepository PageClass { get; private set; }
        public IBlockRepository Block { get; private set; }

    public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
