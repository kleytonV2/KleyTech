using KleyTech.Models;

namespace KleyTech.DataAccess.Data.Repository.IRepository
{
    public interface IBlockRepository : IRepository<Block>
    {
        void Update(Block block);

    }
}
