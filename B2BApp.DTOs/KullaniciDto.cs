using B2BApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Business.Concrete
{
    public class KullaniciDto
    {
        public string Id { get; set; }
        public string KullaniciAdi { get; set; }
        public byte[] SifreSalt { get; set; }
        public byte[] SifreHash { get; set; }
        public Tedarikci Tedarikci{ get; set; }
    }
}
