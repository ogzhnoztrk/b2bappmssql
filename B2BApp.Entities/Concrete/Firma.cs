﻿using B2BApp.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BApp.Entities.Concrete
{

    [Table("TBL_FIRMA_TANIM")]
    public class Firma 
    {
        [Column("firm_id"), Key]
        public Guid FirmaId { get; set; }
        [Column("firm_adi")]
        public string FirmaAdi { get; set; }
        [Column("firm_tel")]
        public string FirmaTel { get; set; }

    }
}
