using B2BApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BApp.Entities.Concrete
{
    [Table("TBL_KULLANICI_TANIM")]
    public class Kullanici 
    {
        [Key, Column("klnc_id")]
        public Guid Id { get; set; }
        [Column("kllc_adi")]
        public string KullaniciAdi { get; set; }
       
        [Column("tdrk_id")]
        public string TedarikciId { get; set; }
        [ForeignKey(nameof(TedarikciId))]
        public Tedarikci Tedarikci { get; set; }

        [Column("kllc_sifre_salt")]
        public byte[] SifreSalt { get; set; }
        [Column("kllc_sifre_hash")]
        public byte[] SifreHash { get; set; }
    }
}
