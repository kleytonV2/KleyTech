using KleyTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KleyTech.DataAccess.Data.Repository.IRepository
{
    public interface IHeaderRepository : IRepository<Header>
    {
        void Update(Header header);

    }
}
