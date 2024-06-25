
using B2BApp.Entities.Abstract;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Entities.Concrete
{
    public class Urun : BaseModel
    {
        public string KategoriId{ get; set; }
        public string UrunAdi { get; set; }
        public string TedarikciId { get; set; }
        public double Fiyat { get; set; }
    }
}
