using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Cadeteria.ViewModels
{
    public class UsuarioViewmodel
    {
        private string usuario;
        private string password;

        [Required(ErrorMessage = "Ingrese un Usuario")]
        public string Usuario { get => usuario; set => usuario = value; }
        [Required(ErrorMessage = "Ingrese una Contraseña")]
        public string Password { get => password; set => password = value; }
    }
}