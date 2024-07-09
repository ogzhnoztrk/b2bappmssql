using B2BApp.Entities.Concrete;

namespace B2BApp.DTOs.FilterDtos
{
    public class SubeUrunTedarikciDto
    {
        public ICollection<Sube> Subeler { get; set; }
        public ICollection<Urun> Urunler { get; set; }
        public ICollection<Tedarikci> Tedarikciler { get; set; }
    }
}
