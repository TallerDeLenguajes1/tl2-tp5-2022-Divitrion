using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cadeteria.Models
{
    public class Persona
    {
        [Required]
        private string nombre;
        [Required]
        private string direccion;
        [Required]
        private string telefono;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }

        public Persona()
        {
            
        }
    }
}