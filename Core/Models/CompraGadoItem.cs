using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Core.Models
{
    [Table("CompraGadoItem")]
    public partial class CompraGadoItem
    {
        [Key]
        public int Id { get; set; }
        public int IdCompraGado { get; set; }
        public int IdAnimal { get; set; }
        public int? Quantidade { get; set; }

        public virtual Animal Animal { get; set; }
        public virtual CompraGado CompraGado { get; set; }
    }
}
