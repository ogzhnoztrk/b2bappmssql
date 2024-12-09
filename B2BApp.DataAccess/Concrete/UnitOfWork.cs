using B2BApp.DataAccess.Abstract;
using B2BApp.DataAccess.Context;
using Core.Models.Concrete.DbSettingsModel;
using Microsoft.Extensions.Options;

namespace B2BApp.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlDbContext _db;


        public UnitOfWork(SqlDbContext db)
        {
            _db = db;
            Firma = new FirmaRepository(_db);
            Kategori = new KategoriRepository(_db);
            Satis = new SatisRepository(_db);
            Sube = new SubeRepository(_db);
            SubeStok = new SubeStokRepository(_db);
            Urun = new UrunRepository(_db);
            Tedarikci = new TedarikciRepository(_db);
            Kullanici = new UserRepository(_db);
            Siparis = new SiparisRepository(_db);
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
