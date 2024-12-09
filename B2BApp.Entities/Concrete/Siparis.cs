using B2BApp.Entities.Abstract;

namespace B2BApp.Entities.Concrete
{
    public class Siparis : BaseModel
    {
        public string SubeId { get; set; }
        public string UrunId { get; set; }
        public string TedarikciId { get; set; }
        public double Adet { get; set; }
        public double Toplam { get; set; }
        public DateTime SiparisTarih { get; set; }
        public bool IsActive { get; set; }
    }
}
