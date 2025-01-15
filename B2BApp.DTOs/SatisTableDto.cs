using B2BApp.Entities.Concrete;

namespace B2BApp.DTOs
{
    public class SatisTableDto
    {
        public string Id { get; set; }
        public string Sube { get; set; }
        public string Urun { get; set; }
        public double SatisMiktari { get; set; }
        public DateTime SatisTarihi { get; set; }
        public double Toplam { get; set; }
    }
}
