using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Models;

namespace Cadeteria.Repositorios
{
    public interface IRepositorioClientes
    {
        public List<Cliente> GetAll();
        public Cliente GetById(int idCliente);
        public List<Pedido> GetPedidos(int idCliente);
        public void Create(Cliente cliente);
        public void Update(Cliente cliente);
        public void Delete(int id);
    }
}