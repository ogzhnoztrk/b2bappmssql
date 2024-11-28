using B2BApp.Core.Models.Concrete.DbSettingsModel;
using B2BApp.DataAccess.Abstract;
using Microsoft.Extensions.Options;

namespace B2BApp.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private static IOptions<MongoSettings> _settings;


        public UnitOfWork(IOptions<MongoSettings> settings)
        {
            _settings = settings;
            Firma = new FirmaRepository(_settings);
            Kategori = new KategoriRepository(_settings);
            Satis = new SatisRepository(_settings);
            Sube = new SubeRepository(_settings);
            SubeStok = new SubeStokRepository(_settings);
            Urun = new UrunRepository(_settings);
            Tedarikci = new TedarikciRepository(_settings);
            Kullanici = new UserRepository(_settings);
            Siparis = new SiparisRepository(_settings);
        }

        public IFirmaRepository Firma { get; private set; }
        public IKategoriRepository Kategori { get; private set; }
        public ISatisRepository Satis { get; private set; }
        public ISubeRepository Sube { get; private set; }
        public ISubeStokRepository SubeStok { get; private set; }
        public IUrunRepository Urun { get; private set; }
        public ITedarikciRepository Tedarikci { get; private set; }
        public IUserRepository Kullanici { get; private set; }
        public ISiparisRepository Siparis { get; private set; }
    }
}
