using B2BApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Entities.Concrete
{
    public class Tedarikci :BaseModel
    {
        public string TedarikciAdi { get; set; }
        public string TedarikciTel { get; set; }

    }
}
