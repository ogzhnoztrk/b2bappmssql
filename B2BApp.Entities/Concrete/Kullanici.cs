using B2BApp.Entities.Abstract;

namespace B2BApp.Entities.Concrete
{
    public class Kullanici : BaseModel
    {
        public string KullaniciAdi { get; set; }
        public string TedarikciId { get; set; }
        public byte[] SifreSalt { get; set; }
        public byte[] SifreHash { get; set; }
    }
}
