using B2BApp.DataAccess.Abstract;
using Core.Models.Concrete.DbSettingsModel;
using DataAccess.Context;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public IFirmaRepository Firma {get; private set;}
        public IKategoriRepository Kategori {get; private set;}
        public ISatisRepository Satis {get; private set;}
        public ISubeRepository Sube {get; private set;}
        public ISubeStokRepository SubeStok {get; private set;}
        public IUrunRepository Urun {get; private set;}
        public ITedarikciRepository Tedarikci {get; private set;}
    }
}
