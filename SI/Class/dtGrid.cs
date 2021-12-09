using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Class
{
    public class dtGrid
    {
        public int idCompraGado { get; set; }
        public string Nome { get; set; }
        public DateTime? Data_Entrega { get; set; }
        public decimal? Valor_Total { get; set; }
    }
}
