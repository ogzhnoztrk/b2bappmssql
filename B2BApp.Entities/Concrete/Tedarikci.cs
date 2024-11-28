using B2BApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BApp.Entities.Concrete
{
    [Table("TBL_TEDARIKCILER")]
    public class Tedarikci 
    {
        [Key, Column("tdrk_id")]
        public Guid Id { get; set; }
        [Column("tdrk_adi")]
        public string TedarikciAdi { get; set; }
        [Column("tdrk_tel")]
        public string TedarikciTel { get; set; }

    }
}
