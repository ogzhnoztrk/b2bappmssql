using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DTOs
{
    public class SubeDto
    {
        public BaseObjectId ObjectId { get; set; }
        public BaseObjectId FirmaId { get; set; }
        public string SubeAdi { get; set; }
        public string SubeTel { get; set; }
    }
}
