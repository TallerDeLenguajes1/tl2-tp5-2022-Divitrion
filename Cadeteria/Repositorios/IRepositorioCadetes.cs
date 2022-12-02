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
        public void Create(Cadete cadete);
        public void Update(Cadete cadete);
        public void Delete(int id);
    }
}