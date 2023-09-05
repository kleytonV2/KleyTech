using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleyTech.DataAccess.Data.Repository.IRepository
{
    public interface IWorkContainer : IDisposable
    {

        ICategoryRepository Category { get; }

        //We have to add the differents repositories here

        void Save();
    }
}
