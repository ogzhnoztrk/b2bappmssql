using B2BApp.Entities.Concrete;
using Core.Models.Concrete.DbSettingsModel;
using DataAccess.Repository;
using Microsoft.Extensions.Options;

namespace B2BApp.DataAccess.Abstract
{
    public class KategoriRepository : MongoRepositoryBase<Kategori>, IKategoriRepository
    {
        public KategoriRepository(IOptions<MongoSettings> settings) : base(settings)
        {
        }
    }
}
