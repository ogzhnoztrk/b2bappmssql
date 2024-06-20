using B2BApp.Entities.Concrete;
using Core.Models.Concrete.DbSettingsModel;
using DataAccess.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DataAccess.Abstract
{
    public class FirmaRepository : MongoRepositoryBase<Firma>, IFirmaRepository
    {
        public FirmaRepository(IOptions<MongoSettings> settings) : base(settings)
        {
        }
    }
}
