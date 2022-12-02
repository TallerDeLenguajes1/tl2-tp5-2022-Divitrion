using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Models;

namespace Cadeteria.ViewModels
{
    public class CadeteViewModel : Persona
    {
        public List<Pedido> listadoPedidos {get; set;}
        public int id {get; set;}
    }
}