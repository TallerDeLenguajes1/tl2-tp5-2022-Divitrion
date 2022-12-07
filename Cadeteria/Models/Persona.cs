using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cadeteria.Models
{
    public class Persona
    {
        private string nombre;
        private string direccion;
        private string telefono;

        [Required]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string Direccion { get => direccion; set => direccion = value; }
        [Required]
        [Phone]
        public string Telefono { get => telefono; set => telefono = value; }

        public Persona()
        {
            
        }
    }
}