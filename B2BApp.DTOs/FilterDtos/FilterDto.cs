using B2BApp.Entities.Concrete;

namespace B2BApp.DTOs.FilterDtos
{
    public class FilterDto
    {
        public ICollection<Firma>? Firmalar { get; set; }
        public ICollection<Sube>? Subeler { get; set; }
        public ICollection<Kategori>? Kategoriler { get; set; }
    }
}
