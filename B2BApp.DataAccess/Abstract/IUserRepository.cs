using B2BApp.Entities.Concrete;
using Core.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DataAccess.Abstract
{
    public interface IUserRepository :IRepository<Kullanici>
    {
    }
}
