using B2BApp.Entities.Abstract;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Entities.Concrete
{
    public class SubeStok : BaseModel
    {
        public ObjectId SubeId{ get; set; }
        public ObjectId UrunId { get; set; }
        public double Stok { get; set; }
    }
}
