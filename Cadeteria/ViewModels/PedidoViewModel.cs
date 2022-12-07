using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.ViewModels
{
    public class PedidoViewModel
    {
        private int cadeteID;
        private int clienteID;
        private string estado;
        private int nro;
        private string nombreCliente;
        private string obs;
        private string direccionCliente;

        [Required]
        public string Estado { get => estado; set => estado = value; }
        [Required]
        public int Nro { get => nro; set => nro = value; }
        [Required]
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public string Obs { get => obs; set => obs = value; }
        [Required]
        public string DireccionCliente { get => direccionCliente; set => direccionCliente = value; }
        [Required]
        public int CadeteID { get => cadeteID; set => cadeteID = value; }
        [Required]
        public int ClienteID { get => clienteID; set => clienteID = value; }
    }
}