using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DTOs
{
    public class SatisDto
    {
        public BaseObjectId ObjectId{ get; set; }
        public BaseObjectId SubeId { get; set; }
        public BaseObjectId UrunId { get; set; }
        public double SatisMiktari { get; set; }
        public DateTime SatisTarihi { get; set; }
        public double Toplam { get; set; }
    }
}
