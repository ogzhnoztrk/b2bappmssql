using B2BApp.Entities.Concrete;
using Core.Models.Concrete.DbSettingsModel;
using DataAccess.Repository;
using Microsoft.Extensions.Options;

namespace B2BApp.DataAccess.Abstract
{
    public class SatisRepository : MongoRepositoryBase<Satis>, ISatisRepository
    {
        public SatisRepository(IOptions<MongoSettings> settings) : base(settings)
        {
        }
    }
}
