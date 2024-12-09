
using B2BApp.Entities.Abstract;

namespace B2BApp.Entities.Concrete
{
    public class Urun : BaseModel
    {
        public string KategoriId { get; set; }
        public string UrunAdi { get; set; }
        public string TedarikciId { get; set; }
        public double Fiyat { get; set; }
        public double? SatisFiyati { get; set; }
    }
}
