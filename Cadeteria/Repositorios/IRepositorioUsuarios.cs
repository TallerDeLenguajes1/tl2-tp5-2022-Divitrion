using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Models;
using Microsoft.Data.Sqlite;

namespace Cadeteria.Repositorios
{
    public interface IRepositorioUsuarios
    {
         public Usuario getUser(string username, string password);
         public void CreateUser(string nombre, int rol, string usuario, string password);
         public void Delete(int id);
    }
}