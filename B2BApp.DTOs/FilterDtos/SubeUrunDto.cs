using B2BApp.Entities.Concrete;

namespace B2BApp.DTOs.FilterDtos
{
    public class SubeUrunDto
    {
        public ICollection<Sube> Subeler { get; set; }
        public ICollection<Urun> Urunler { get; set; }
    }
}
