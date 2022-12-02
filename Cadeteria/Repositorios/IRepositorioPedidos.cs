using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Models;

namespace Cadeteria.Repositorios
{
    public interface IRepositorioPedidos
    {
        public List<Pedido> GetAll();
        public Pedido GetById(int idPedido);
        public void Create(Pedido pedido);
        public void Update(Pedido pedido);
        public void Delete(int id);
    }
}