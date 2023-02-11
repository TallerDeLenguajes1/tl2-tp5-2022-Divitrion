using Cadeteria.Models;
using Cadeteria.ViewModels;
using Microsoft.Data.Sqlite;

namespace Cadeteria.Repositorios
{
    public class RepositorioClientes : IRepositorioClientes
    {
        private string cadenaConexion = "Data Source=DB/PedidosDB.db;Cache=Shared";

        public List<Cliente> GetAll()
        {
            var queryString = $"SELECT * FROM Clientes WHERE Activo = {1};";
            List<Cliente> clientes = new List<Cliente>();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
            
                using(SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cliente = new Cliente();
                        cliente.Id = Convert.ToInt32(reader["id"]);
                        cliente.Direccion = reader["Direccion"].ToString();
                        cliente.Nombre = reader["Nombre"].ToString();
                        cliente.Telefono = reader["Telefono"].ToString();
                        clientes.Add(cliente);
                    }
                }
                connection.Close();
            }
            return clientes;
        }
        public Cliente GetById(int idCliente)
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                SqliteDataReader lector;
                var cliente = new Cliente();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM Clientes WHERE id = '{idCliente}';";
                connection.Open();
                using(SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cliente.Id = Convert.ToInt32(reader["id"]);
                        cliente.Direccion = reader["Direccion"].ToString();
                        cliente.Nombre = reader["Nombre"].ToString();
                        cliente.Telefono = reader["Telefono"].ToString();
                    }
                }
                connection.Close();

                return (cliente);
            }

        public void Create(Cliente cliente)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO Clientes (Nombre, Direccion, Telefono, Activo) VALUES ('{cliente.Nombre}', '{cliente.Direccion}', '{cliente.Telefono}', '{1}');";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Cliente cliente)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Clientes SET Nombre = '{cliente.Nombre}', Direccion = '{cliente.Direccion}', Telefono = '{cliente.Telefono}' WHERE id = '{cliente.Id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(int id)
        {

            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Clientes SET Activo = {0} WHERE id = '{id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
