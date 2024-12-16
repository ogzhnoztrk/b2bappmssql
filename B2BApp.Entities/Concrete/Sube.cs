using B2BApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BApp.Entities.Concrete
{
    [Table("TBL_SUBE_TANIM")]
    public class Sube 
    {
        [Column("sube_id"), Key]
        public Guid Id { get; set; }

        [Column("firm_id")]
        public Guid FirmaId { get; set; }
        [ForeignKey(nameof(FirmaId))]
        public virtual Firma? Firma { get; set; }

        [Column("sube_adi")]
        public string SubeAdi { get; set; }
        [Column("sube_tel")]
        public string SubeTel { get; set; }
    }
}
