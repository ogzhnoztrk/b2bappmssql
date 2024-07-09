using B2BApp.Entities.Concrete;

namespace B2BApp.DTOs
{
    public class UrunDto
    {

        public string Id { get; set; }
        public string UrunAdi { get; set; }
        public double Fiyat { get; set; }
        public double? SatisFiyati { get; set; }
        public Kategori Kategori { get; set; }
        public Tedarikci Tedarikci { get; set; }



        //public Urun Urun { get; set; }
        //public Kategori Kategori{ get; set; }

    }
}
