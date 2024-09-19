using KleyTech.Models;

namespace KleyTech.DataAccess.Data.Repository.IRepository
{
    public interface ISliderRepository : IRepository<Slider>
    {
        void Update(Slider slider);

    }
}
