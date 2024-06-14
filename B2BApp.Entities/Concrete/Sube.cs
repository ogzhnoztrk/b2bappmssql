using B2BApp.Entities.Abstract;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Entities.Concrete
{
    public class Sube : BaseModel
    {

        public ObjectId FirmaId{ get; set; }
        public string SubeAdi { get; set; }
    }
}
