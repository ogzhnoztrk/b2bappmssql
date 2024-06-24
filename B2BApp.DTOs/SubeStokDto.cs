using B2BApp.Entities.Concrete;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DTOs
{
    public class SubeStokDto
    {
        public string id{ get; set; }
        public Sube Sube { get; set; }
        public Urun Urun { get; set; }
        public double Stok { get; set; }
    }
}
