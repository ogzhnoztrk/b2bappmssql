using B2BApp.Entities.Concrete;

namespace B2BApp.DTOs
{
    public class SiparisDto
    {
        public string Id { get; set; }
        public Sube Sube { get; set; }
        public Urun Urun { get; set; }
        public Tedarikci Tedarikci { get; set; }
        public double Adet { get; set; }
        public double Toplam { get; set; }
        public DateTime Tarih { get; set; }
        public bool IsActive { get; set; }
    }
}
