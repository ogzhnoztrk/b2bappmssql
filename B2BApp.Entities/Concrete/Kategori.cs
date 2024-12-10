using B2BApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BApp.Entities.Concrete
{
    [Table("TBL_KATEGORI_TANIM")]
    public class Kategori 
    {
        [Column("ktgr_id"), Key]
        public Guid KategoriId { get; set; }
        [Column("ktgr_adi")]
        public string KategoriAdi { get; set; }
    }
}
