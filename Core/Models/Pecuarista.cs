using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Core.Models
{
    public partial class Pecuarista
    {

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }

        //public virtual ICollection<CompraGado> CompraGado { get; set; }
    }
}
