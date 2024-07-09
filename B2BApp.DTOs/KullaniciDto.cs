using B2BApp.Entities.Concrete;

namespace B2BApp.Business.Concrete
{
    public class KullaniciDto
    {
        public string Id { get; set; }
        public string KullaniciAdi { get; set; }
        public byte[] SifreSalt { get; set; }
        public byte[] SifreHash { get; set; }
        public Tedarikci Tedarikci { get; set; }
    }
}
