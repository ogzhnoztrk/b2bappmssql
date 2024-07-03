using B2BApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DTOs
{
    public class FilterDto
    {
        public ICollection<Firma>? Firmalar { get; set; }
        public ICollection<Sube>? Subeler{ get; set; }
        public ICollection<Kategori>? Kategoriler { get; set; }
    }
}
