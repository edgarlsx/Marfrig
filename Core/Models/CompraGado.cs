using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Core.Models
{
    [Table("CompraGado")]
    public partial class CompraGado
    {

        [Key]
        public int Id { get; set; }
        public int IdPecuarista { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DataEntrega { get; set; }

        public virtual Pecuarista Pecuarista { get; set; }
        public virtual ICollection<CompraGadoItem> CompraGadoItem { get; set; }
    }
}
