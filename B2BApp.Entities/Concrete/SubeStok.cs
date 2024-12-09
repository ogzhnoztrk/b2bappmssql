using B2BApp.Entities.Abstract;

namespace B2BApp.Entities.Concrete
{
    public class SubeStok : BaseModel
    {
        public string SubeId { get; set; }
        public string UrunId { get; set; }
        public double Stok { get; set; }
    }
}
