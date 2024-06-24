using B2BApp.Entities.Concrete;
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
        public string Id{ get; set; }
        public string SubeAdi { get; set; }
        public string SubeTel { get; set; }
        public Firma Firma { get; set; }

    }
}
