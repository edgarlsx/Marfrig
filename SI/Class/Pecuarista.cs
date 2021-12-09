using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Class
{
    public class Pecuarista
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Nome { get; set; }

    }
}
