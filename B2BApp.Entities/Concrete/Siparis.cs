using B2BApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Entities.Concrete
{
    public class Siparis : BaseModel
    {
        public string SubeId { get; set; }
        public string UrunId { get; set; }
        public string TedarikciId { get; set; }
        public double Adet { get; set; }
        public double Toplam { get; set; }
        public DateTime SiparisTarih { get; set; }
    }
}
