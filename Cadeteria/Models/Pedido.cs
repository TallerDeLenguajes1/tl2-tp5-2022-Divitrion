using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class Pedido
    {
        private static int id=0;
        private int nro;
        private string obs;
        private Cliente cliente;
        private string estado;

        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public string Estado { get => estado; set => estado = value; }

        public Pedido()
        {
            id++;
            nro = id;
            this.Cliente = new Cliente();
            this.Estado = "Pendiente";
        }
    }
}