using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Models;

namespace Cadeteria.Repositorios
{
    public interface IRepositorioCadetes
    {
        public List<Cadete> GetAll();
        public Cadete GetById(int idCadete);
        public List<Pedido> GetPedidos(int idCadete);
        public Cadete GetUser(int idUsuario);
        public void Create(Cadete cadete);
        public void Update(Cadete cadete);
        public void Delete(int id);
    }
}