using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Repositorios
{
    public class RepositorioClientes : IRepositorioClientes
    {
        private string cadenaConexion = "Data Source=DB/PedidosDB.db;Cache=Shared";
        
        public Cliente GetById(int idcliente)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteDataReader lector;
            var cliente = new Cliente();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Clientes WHERE id = '{idcliente}';";
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cliente.id = Convert.ToInt32(reader["id"]);
                    cliente.Nombre = reader["Nombre"].ToString();
                    pedido.Estado = reader["Estado"].ToString();
                }
            }
            connection.Close();

            return (pedido);

        }

        public void Create(Cliente cliente)
        {

        }

        public void Update(Cliente cliente)
        {

        }

        public void Delete(int id)
        {

        }
    }
}