using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.DTOs
{
    public class BaseObjectId
    {
        public int TimeStamp { get; set; }
        public int Machine { get; set; }
        public int Increment { get; set; }
        public int Pid { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
