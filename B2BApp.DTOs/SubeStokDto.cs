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
        public BaseObjectId ObjectId { get; set; }
        public BaseObjectId SubeId { get; set; }
        public BaseObjectId UrunId { get; set; }
        public double Stok { get; set; }
    }
}
