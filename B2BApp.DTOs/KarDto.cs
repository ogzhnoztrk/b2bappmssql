using B2BApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DTOs
{
    public class KarDto
    {
        public Sube Sube{ get; set; }
        public Urun Urun{ get; set; }
        public Firma Firma { get; set; }
        public double ToplamSatisMiktari { get; set; }
        public double ToplamFiyat { get; set; }
        public double ToplamSatisFiyat { get; set; }
        public double ToplamKar { get; set; }
    }
}
