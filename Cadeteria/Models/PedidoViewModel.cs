using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class PedidoViewModel
    {
        private Pedido pedido;
        private Cadete cadete;

        public Pedido Pedido { get => pedido; set => pedido = value; }
        public Cadete Cadete { get => cadete; set => cadete = value; }
    }
}