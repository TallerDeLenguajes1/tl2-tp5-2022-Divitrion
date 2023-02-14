using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cadeteria.ViewModels
{
    public class CadeteViewModel
    {
        [ValidateNever]
        public List<Pedido> listadoPedidos {get; set;}
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "La direccion es obligatoria")]
        public string Direccion { get => direccion; set => direccion = value; }
        [Required(ErrorMessage = "El telefono es obligatorio")]
        [Phone(ErrorMessage = "Ingrese un numero valido")]
        public string Telefono { get => telefono; set => telefono = value; }
        public int Id { get => id; set => id = value; }
    }
}