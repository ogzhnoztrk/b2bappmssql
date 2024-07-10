using B2BApp.Entities.Concrete;

namespace B2BApp.DTOs
{
    public class KarsilastirmaliSatisRaporDto
    {

        public Urun? Urun { get; set; }
        public double? Donem1Miktar { get; set; }
        public double? Donem1Tutar { get; set; }
        public double? Donem2Miktar { get; set; }
        public double? Donem2Tutar { get; set; }

    }
    public class DonemselToplam
    {
        public Dictionary<DateTime, double> Donem1 { get; set; }
        public Dictionary<DateTime, double> Donem2 { get; set; }
    }

    public class KarsilastirmaliSatisRapor
    {
        public List<KarsilastirmaliSatisRaporDto> KarsilastirmaliSatisRaporDtos { get; set; }
        public DonemselToplam DonemselToplam { get; set; }
        public string Donem1Tarih { get; set; }
        public string Donem2Tarih { get; set; }
    }


}
