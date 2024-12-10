using B2BApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BApp.Entities.Concrete
{
    [Table("TBL_SIPARIS")]
    public class Siparis 
    {
        [Column("sprs_id"), Key]
        public int SiparisId { get; set; }

        [Column("sube_id")]
        public Guid SubeId { get; set; }
        [ForeignKey(nameof(SubeId))]
        public virtual Sube Sube { get; set; }

        [Column("urun_id")]
        public Guid UrunId { get; set; }
        [ForeignKey(nameof(UrunId))]
        public virtual Urun Urun { get; set; }

        [Column("tdrk_id")]
        public Guid TedarikciId { get; set; }
        [ForeignKey(nameof(TedarikciId))]
        public virtual Tedarikci Tedarikci { get; set; }

        [Column("sprs_adet")]
        public double Adet { get; set; }
        [Column("sprs_toplam")]
        public double Toplam { get; set; }
        [Column("sprs_tarih")]
        public DateTime SiparisTarih { get; set; }
        [Column("sprs_is_active")]
        public bool IsActive { get; set; }
    }
}
