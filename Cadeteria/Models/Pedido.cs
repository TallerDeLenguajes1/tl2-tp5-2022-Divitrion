using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class Pedido
    {
        private int nro;
        private string obs;
        private Cliente cliente;
        private string estado;
        private int cadeteID;
        private int clienteID;

        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public string Estado { get => estado; set => estado = value; }
        public int CadeteID { get => cadeteID; set => cadeteID = value; }
        public int ClienteID { get => clienteID; set => clienteID = value; }

        public Pedido()
        {
        }
    }
}