using B2BApp.Entities.Concrete;

namespace B2BApp.DTOs
{
    public class SubeStokDto
    {
        public string id { get; set; }
        public Sube Sube { get; set; }
        public Urun Urun { get; set; }
        public double Stok { get; set; }
    }
}
