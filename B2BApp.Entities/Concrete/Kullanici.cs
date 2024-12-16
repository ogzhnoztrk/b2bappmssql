using B2BApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BApp.Entities.Concrete
{
    [Table("TBL_KULLANICI_TANIM")]
    public class Kullanici 
    {
        [Column("klnc_id"), Key]
        public Guid Id { get; set; }
        [Column("klnc_adi")]
        public string KullaniciAdi { get; set; }

        [Column("tdrk_id")]
        public Guid TedarikciId { get; set; }
        [ForeignKey(nameof(TedarikciId))]
        public virtual Tedarikci? Tedarikci{ get; set; }

        [Column("klnc_sifre_salt")]
        public byte[] SifreSalt { get; set; }
        [Column("klnc_sifre_hash")]
        public byte[] SifreHash { get; set; }
    }
}
