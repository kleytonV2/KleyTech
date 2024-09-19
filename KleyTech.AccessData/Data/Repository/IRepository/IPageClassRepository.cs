using KleyTech.Models;

namespace KleyTech.DataAccess.Data.Repository.IRepository
{
    public interface IPageClassRepository : IRepository<PageClass>
    {
        void Update(PageClass pageClass);

    }
}
