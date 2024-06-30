using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DTOs
{
	public class UrunlerVeAylikSatislarDto
	{
		public Dictionary<string, double> ToplamAySatislar { get; set; }
        public List<UrunDto> Urunler { get; set; }
    }
}
