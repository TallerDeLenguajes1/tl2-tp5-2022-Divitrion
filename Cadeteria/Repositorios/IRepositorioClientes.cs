using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Repositorios
{
    public interface IRepositorioClientes
    {
        public Cliente GetById(int idcliente);
        public void Create(Cliente cliente);
        public void Update(Cliente cliente);
        public void Delete(int id);
    }
}