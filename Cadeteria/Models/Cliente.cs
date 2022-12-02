using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class Cliente : Persona
    {
        public string DatoReferenciaDireccion;
        private int id;


        public Cliente()
        {
        }

        public int Id { get => id; set => id = value; }
    }
}