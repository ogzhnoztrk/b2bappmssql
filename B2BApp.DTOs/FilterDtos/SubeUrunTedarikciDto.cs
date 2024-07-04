using B2BApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DTOs.FilterDtos
{
    public class SubeUrunTedarikciDto
    {
        public ICollection<Sube> Subeler{ get; set; }
        public ICollection<Urun> Urunler{ get; set; }
        public ICollection<Tedarikci> Tedarikciler{ get; set; }
    }
}
