using B2BApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Entities.Concrete
{
    public class Kullanici : BaseModel
    {
        public string KullaniciAdi { get; set; }
        public string TedarikciId { get; set; }
        public byte[] SifreSalt { get; set; }
        public byte[] SifreHash { get; set; }
    }
}
