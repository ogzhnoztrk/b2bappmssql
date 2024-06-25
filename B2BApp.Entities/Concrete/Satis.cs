using B2BApp.Entities.Abstract;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Entities.Concrete
{
    public class Satis : BaseModel
    {
        public string SubeId{ get; set; }
        public string UrunId{ get; set; }
        public double SatisMiktari { get; set; }
        public DateTime SatisTarihi { get; set; }
        public double Toplam { get; set; }

    }
}
