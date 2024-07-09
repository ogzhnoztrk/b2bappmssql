using B2BApp.Entities.Concrete;

namespace B2BApp.DTOs
{
    public class SubeDto
    {
        public string Id { get; set; }
        public string SubeAdi { get; set; }
        public string SubeTel { get; set; }
        public Firma Firma { get; set; }

    }
}
