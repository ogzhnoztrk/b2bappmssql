using B2BApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BApp.Entities.Concrete
{
    [Table("TBL_SUBE_STOK")]
    public class SubeStok 
    {
        [Column("sstk_"),Key]
        public Guid SubeStokId { get; set; }

        [Column("sube_id")]
        public Guid SubeId { get; set; }
        [ForeignKey(nameof(SubeId))]
        public virtual Sube Sube { get; set; }

        [Column("urun_id")]
        public Guid UrunId { get; set; }
        [ForeignKey(nameof(UrunId))]
        public virtual Urun Urun { get; set; }

        [Column("sstk_stok_adet")]
        public double Stok { get; set; }
    }
}
