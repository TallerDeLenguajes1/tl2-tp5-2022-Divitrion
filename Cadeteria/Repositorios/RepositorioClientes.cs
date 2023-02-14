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
            try
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
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public Cliente GetById(int idCliente)
            {
                try
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

                    if (cliente.Nombre == null)
                    {
                        throw new Exception();
                    }

                    return (cliente);
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
            }

        public List<Pedido> GetPedidos(int idCliente)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                var pedidos = new List<Pedido>();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM Pedidos WHERE ClienteId = '{idCliente}' AND Activo = {1};";
                connection.Open();
                using(SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pedido = new Pedido();
                        pedido.Nro = Convert.ToInt32(reader["Nro"]);
                        pedido.Obs = reader["Obs"].ToString();
                        pedido.Estado = reader["Estado"].ToString();
                        pedido.CadeteID = Convert.ToInt32(reader["CadeteId"]);
                        pedido.ClienteID = Convert.ToInt32(reader["ClienteId"]);
                        pedidos.Add(pedido);
                    }
                }
                connection.Close();

                return pedidos;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Create(Cliente cliente)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO Clientes (Nombre, Direccion, Telefono, Activo) VALUES ('{cliente.Nombre}', '{cliente.Direccion}', '{cliente.Telefono}', '{1}');";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Update(Cliente cliente)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"UPDATE Clientes SET Nombre = '{cliente.Nombre}', Direccion = '{cliente.Direccion}', Telefono = '{cliente.Telefono}' WHERE id = '{cliente.Id}';";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"UPDATE Clientes SET Activo = {0} WHERE id = '{id}';";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
