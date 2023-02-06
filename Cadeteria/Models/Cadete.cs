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
        private int userId;
        protected static int id = 0;

        public int UserId { get => userId; set => userId = value; }

        public void JornalACobrar()
        {
            
        }

        public Cadete()
        {
            this.Id=id++;
            this.listadoPedidos=new List<Pedido>();
        }
    }
}