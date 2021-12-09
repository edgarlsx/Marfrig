using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Core.Models
{
    [Table("Animal")]
    public partial class Animal
    {

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Descricao { get; set; }
        [Column(TypeName = "decimal(15, 2)")]
        public decimal? Preco { get; set; }

        public virtual ICollection<CompraGadoItem> CompraGadoItems { get; set; }
    }
}
