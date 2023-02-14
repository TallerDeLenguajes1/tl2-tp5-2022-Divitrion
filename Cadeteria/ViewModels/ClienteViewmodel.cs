using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cadeteria.ViewModels
{
    public class ClienteViewmodel
    {
        private string nombre;
        private string telefono;
        private string direccion;
        private int id;

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "El Telefono es obligatorio")][Phone(ErrorMessage = "Ingrese un numero de telefono valido")]
        public string Telefono { get => telefono; set => telefono = value; }
        [Required(ErrorMessage = "La Direccion es obligatoria")]
        public string Direccion { get => direccion; set => direccion = value; }
        [ValidateNever]
        public int Id { get => id; set => id = value; }
    }
}