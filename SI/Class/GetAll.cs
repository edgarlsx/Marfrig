using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Class
{
    public class GetAll
    {
        public Int32 IdPecuarista { get; set; }
        public string Nome { get; set; }
        public Int32 IdCompraGado { get; set; }
        public DateTime? DataEntrega { get; set; }
        public Int32 IdCompraGadoItem { get; set; }
        public Int32 Quantidade { get; set; }
        public Int32 IdAnimal { get; set; }
        public string Descricao { get; set; }
        public decimal? Preco { get; set; }
    }
}
