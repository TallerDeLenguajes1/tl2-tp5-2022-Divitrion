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

        [Required]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string Direccion { get => direccion; set => direccion = value; }
        [Required]
        [Phone]
        public string Telefono { get => telefono; set => telefono = value; }
        public int Id { get => id; set => id = value; }
    }
}