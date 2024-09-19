using KleyTech.Models;

namespace KleyTech.DataAccess.Data.Repository.IRepository
{
    public interface IHeaderRepository : IRepository<Header>
    {
        void Update(Header header);

    }
}
