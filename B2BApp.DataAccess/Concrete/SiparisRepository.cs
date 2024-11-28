using B2BApp.Core.Models.Concrete.DbSettingsModel;
using B2BApp.DataAccess.Abstract;
using B2BApp.DataAccess.Repository;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.Extensions.Options;

namespace B2BApp.DataAccess.Concrete
{
    public class SiparisRepository : MongoRepositoryBase<Siparis>, ISiparisRepository
    {
        public SiparisRepository(IOptions<MongoSettings> settings) : base(settings)
        {
        }

    }
}
