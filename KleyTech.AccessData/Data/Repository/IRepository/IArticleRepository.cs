using KleyTech.Models;

namespace KleyTech.DataAccess.Data.Repository.IRepository
{
    public interface IArticleRepository : IRepository<Article>
    {
        void Update(Article article);

    }
}
