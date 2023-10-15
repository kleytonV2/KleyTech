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
            dbObject.Name = block.Name;
            dbObject.Status = block.Status;
            dbObject.HTML_Content = block.HTML_Content;
            dbObject.Order = block.Order;
        }
    }
}
