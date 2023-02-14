using Cadeteria.Models;
using Cadeteria.ViewModels;
using Microsoft.Data.Sqlite;

namespace Cadeteria.Repositorios
{
    public class RepositorioCadetes : IRepositorioCadetes
    {
        private string cadenaConexion = "Data Source=DB/PedidosDB.db;Cache=Shared";

        public RepositorioCadetes()
        {
        }

        public List<Cadete> GetAll()
        {
            try
            {
                 var queryString = $"SELECT * FROM Cadetes WHERE Activo = {1};";
                List<Cadete> cadetes = new List<Cadete>();
                using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    SqliteCommand command = new SqliteCommand(queryString, connection);
                    connection.Open();
                
                    using(SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cadete = new Cadete();
                            cadete.Id = Convert.ToInt32(reader["id"]);
                            cadete.Direccion = reader["Direccion"].ToString();
                            cadete.Nombre = reader["Nombre"].ToString();
                            cadete.Telefono = reader["Telefono"].ToString();
                            cadetes.Add(cadete);
                        }
                    }
                    connection.Close();
                }
                return cadetes;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Cadete GetById(int idCadete)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                var cadete = new Cadete();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM Cadetes WHERE id = '{idCadete}';";
                connection.Open();
                using(SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cadete.Id = Convert.ToInt32(reader["id"]);
                        cadete.Direccion = reader["Direccion"].ToString();
                        cadete.Nombre = reader["Nombre"].ToString();
                        cadete.Telefono = reader["Telefono"].ToString();
                        cadete.UserId = Convert.ToInt32(reader["idUsuario"]);
                    }
                }
                connection.Close();
                if (cadete.Nombre == null)
                {
                    throw new Exception();
                }
                return (cadete);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        
        public List<Pedido> GetPedidos(int idCadete)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                var pedidos = new List<Pedido>();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM Pedidos WHERE CadeteId = '{idCadete}' AND Activo = {1};";
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

        public void Create(Cadete cadete)
        {
            try
            {
                var query = $"INSERT INTO Cadetes (Nombre, Direccion, Telefono, idUsuario, Activo) VALUES (@Nombre,@Direccion,@Telefono,@idUsuario,@Activo)";
                using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();
                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@Nombre", cadete.Nombre));
                    command.Parameters.Add(new SqliteParameter("@Direccion", cadete.Direccion));
                    command.Parameters.Add(new SqliteParameter("@Telefono", cadete.Telefono));
                    command.Parameters.Add(new SqliteParameter("@idUsuario", cadete.UserId));
                    command.Parameters.Add(new SqliteParameter("@Activo", 1));

                    command.ExecuteNonQuery();

                    connection.Close();   
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }

        public Cadete GetUser(int idUsuario)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                var cadete = new Cadete();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM Cadetes WHERE idUsuario = '{idUsuario}';";
                connection.Open();
                using(SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cadete.Id = Convert.ToInt32(reader["id"]);
                        cadete.Direccion = reader["Direccion"].ToString();
                        cadete.Nombre = reader["Nombre"].ToString();
                        cadete.Telefono = reader["Telefono"].ToString();
                    }
                }
                connection.Close();

                return (cadete);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Update(Cadete cadete)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"UPDATE Cadetes SET Nombre = '{cadete.Nombre}', Direccion = '{cadete.Direccion}', Telefono = '{cadete.Telefono}' WHERE id = '{cadete.Id}';";
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
                command.CommandText = $"UPDATE Cadetes SET Activo = {0} WHERE id = '{id}';";
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
