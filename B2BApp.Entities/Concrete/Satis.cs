using B2BApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BApp.Entities.Concrete
{
    [Table("TBL_SATIS")]
    public class Satis 
    {
        [Column("sats_id"), Key]
        public int Id { get; set; }

        [Column("sube_id")]
        public Guid SubeId { get; set; }
        [ForeignKey(nameof(SubeId))]
        public virtual Sube? Sube{ get; set; }

        [Column("urun_id")]
        public Guid UrunId { get; set; }
        [ForeignKey(nameof(UrunId))]
        public virtual Urun?  Urun { get; set; }

        [Column("sats_miktari")]
        public double SatisMiktari { get; set; }
        [Column("sats_tarihi")]
        public DateTime SatisTarihi { get; set; }
        [Column("sats_toplam")]
        public double Toplam { get; set; }

    }
}
