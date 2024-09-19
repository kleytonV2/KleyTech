using KleyTech.Data;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.Models;

namespace KleyTech.DataAccess.Data.Repository
{
    internal class BlockRepository : Repository<Block>, IBlockRepository
    {
        private readonly ApplicationDbContext _db;
        public BlockRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Block block)
        {
            var dbObject = _db.Blocks.FirstOrDefault(i => i.Id == block.Id);
            if (dbObject is not null)
            {
                dbObject.Name = block.Name;
                dbObject.Status = block.Status;
                dbObject.HTML_Content = block.HTML_Content;
                dbObject.Order = block.Order;
            }
        }
    }
}
