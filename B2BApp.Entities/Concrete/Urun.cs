
using B2BApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BApp.Entities.Concrete
{
    [Table("TBL_URUN_TANIM")]
    public class Urun 
    {
        [Column("urun_id"), Key]
        public Guid? Id { get; set; }

        [Column("ktgr_id")]
        public Guid KategoriId { get; set; }
        [ForeignKey(nameof(KategoriId))]
        public virtual Kategori? Kategori { get; set; }

        [Column("urun_adi")]
        public string UrunAdi { get; set; }

        [Column("tdrk_id")]
        public Guid TedarikciId { get; set; }
        [ForeignKey(nameof(TedarikciId))]
        public virtual Tedarikci? Tedarikci { get; set; }

        [Column("urun_fiyat")]
        public double Fiyat { get; set; }
        [Column("urun_satis_fiyat")]
        public double SatisFiyati { get; set; }
    }
}
