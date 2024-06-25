using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DataAccess.Abstract
{
    public interface IUnitOfWork
    {
        public IFirmaRepository Firma { get; }
        public IKategoriRepository Kategori{ get; }
        public ISatisRepository Satis{ get; }
        public ISubeRepository Sube { get; }
        public ISubeStokRepository SubeStok { get; }
        public IUrunRepository Urun { get; }
        public ITedarikciRepository Tedarikci { get; }
    }
}
