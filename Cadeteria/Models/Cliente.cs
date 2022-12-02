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

        public int Id { get => id; set => nro = id; }


        public Cliente()
        {
        }
    }
}