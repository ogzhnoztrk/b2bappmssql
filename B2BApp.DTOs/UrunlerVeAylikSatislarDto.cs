namespace B2BApp.DTOs
{
    public class UrunlerVeAylikSatislarDto
    {
        public Dictionary<string, double> ToplamAySatislar { get; set; }
        public Dictionary<string, double> ToplamUrunSatis { get; set; }
        public List<UrunDto> Urunler { get; set; }
    }
}
