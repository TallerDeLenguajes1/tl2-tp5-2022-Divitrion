using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.ViewModels
{
    public class ClienteViewmodel
    {
        private string nombre;
        private string telefono;
        private string direccion;
        private int id;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Id { get => id; set => id = value; }
    }
}