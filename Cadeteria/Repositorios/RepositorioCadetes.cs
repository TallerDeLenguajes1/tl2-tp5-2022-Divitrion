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
            var queryString = @"SELECT * FROM Cadetes;";
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
                        cadetes.Add(cadete);
                    }
                }
                connection.Close();
            }
            return cadetes;
        }

        public Cadete GetById(int idCadete)
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                SqliteDataReader lector;
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
                    }
                }
                connection.Close();

                return (cadete);
            }

        public void Create(Cadete cadete)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO Cadetes (Nombre, Direccion, Telefono) VALUES ('{cadete.Nombre}', '{cadete.Direccion}', '{cadete.Telefono}');";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

            public void Update(Cadete cadete)
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"UPDATE Cadetes SET Nombre = '{cadete.Nombre}', Direccion = '{cadete.Direccion}', Telefono = '{cadete.Telefono}' WHERE IdCadete = '{cadete.Id}';";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            public void Delete(int id)
            {

                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM Cadetes WHERE id = '{id}';";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
    }
}
