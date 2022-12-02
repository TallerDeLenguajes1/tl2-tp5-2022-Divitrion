using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.ViewModels
{
    public class PedidoViewModel
    {
        private int idCadete;
        private string estado;
        private int nro;
        private string nombreCliente;
        private string obs;
        private string direccionCliente;
        private string nombreCadete;

        [Required]
        public int IdCadete { get => idCadete; set => idCadete = value; }
        [Required]
        public string Estado { get => estado; set => estado = value; }
        [Required]
        public int Nro { get => nro; set => nro = value; }
        [Required]
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public string Obs { get => obs; set => obs = value; }
        [Required]
        public string DireccionCliente { get => direccionCliente; set => direccionCliente = value; }
        public string NombreCadete { get => nombreCadete; set => nombreCadete = value; }
    }
}