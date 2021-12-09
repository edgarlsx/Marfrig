using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class SendAllDto
    {
        public int IdPecuarista { get; set; }
        public string Nome { get; set; }
        public int IdCompraGado { get; set; }
        public DateTime? DataEntrega { get; set; }
        public int IdCompraGadoItem { get; set; }
        public int Quantidade { get; set; }
        public int IdAnimal { get; set; }
        public string Descricao { get; set; }
        public decimal? Preco { get; set; }
        public string Message { get; set; }
    }
}
