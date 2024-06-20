using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DTOs
{
    public class UrunDto
    {
        public BaseObjectId BaseObjectId { get; set; }
        public BaseObjectId KategoriId { get; set; }
        public string UrunAdi { get; set; }
        public double Fiyat { get; set; }
    }
}
