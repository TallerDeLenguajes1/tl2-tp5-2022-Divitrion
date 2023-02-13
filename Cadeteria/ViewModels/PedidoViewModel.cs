using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        [Required(ErrorMessage = "Estado no valido")]
        public string Estado { get => estado; set => estado = value; }
        [Required(ErrorMessage = "Ingrese una Observacion")]
        public string Obs { get => obs; set => obs = value; }
        [Required(ErrorMessage = "Por favor ingrese un Cadete Valido")]
        public int CadeteID { get => cadeteID; set => cadeteID = value; }
        [Required(ErrorMessage = "Por favor ingrese un Cliente Valido")]
        public int ClienteID { get => clienteID; set => clienteID = value; }
        [ValidateNever]
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        [ValidateNever]
        public string DireccionCliente { get => direccionCliente; set => direccionCliente = value; }
        [ValidateNever]
        public int Nro { get => nro; set => nro = value; }
    }
}