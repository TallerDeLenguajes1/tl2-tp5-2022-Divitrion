using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class Cadete : Persona
    {
        public List<Pedido> listadoPedidos;
        public int Id;
        protected static int id = 0;

        public void JornalACobrar()
        {
            
        }

        public Cadete()
        {
            this.Id=id++;
            this.listadoPedidos=new List<Pedido>();
        }

        public void recibirPedido(Pedido pedido)
        {
            listadoPedidos.Add(pedido);
        }
    }
}