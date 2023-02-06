using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cadeteria.Models
{
    public class Usuario
    {
        private int id;
        private string nombre;
        private string username;
        private int rol;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Username { get => username; set => username = value; }
        public int Rol { get => rol; set => rol = value; }

        public Usuario()
        {
            id = -1;
            nombre = "";
            username = "";
        }
    }
}