using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Models;
using Microsoft.Data.Sqlite;

namespace Cadeteria.Repositorios
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private string cadenaConexion = "Data Source=DB/PedidosDB.db;Cache=Shared";
        public Usuario getUser(string username, string password)
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(cadenaConexion);
                SqliteCommand command = connection.CreateCommand();
                var usuario = new Usuario();
                command.CommandText = $"SELECT id, nombre, usuario, rol FROM Usuarios WHERE usuario = '{username}' AND password = '{password}' AND Activo = {1}";
                connection.Open();
                using(SqliteDataReader reader = command.ExecuteReader())
                {
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre = reader["nombre"].ToString();
                    usuario.Rol = Convert.ToInt32(reader["rol"]);
                    usuario.Username = reader["usuario"].ToString();
                }
            }
            connection.Close();

            if (usuario.Nombre == null)
                {
                    throw new Exception();
                }

            return usuario;
                
            }
            catch (System.Exception)
            {
                return new Usuario();
            }
        }

        public void CreateUser(string nombre, int rol, string usuario, string password)
        {
            try
            {
                var query = $"INSERT INTO Usuarios (nombre, usuario, password, rol, Activo) VALUES (@nombre,@usuario,@password,@rol,@activo)";
                using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();
                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@nombre", nombre));
                    command.Parameters.Add(new SqliteParameter("@rol", rol));
                    command.Parameters.Add(new SqliteParameter("@usuario", usuario));
                    command.Parameters.Add(new SqliteParameter("@password", password));
                    command.Parameters.Add(new SqliteParameter("@activo", 1));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
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
                command.CommandText = $"UPDATE Usuarios SET Activo = {0} WHERE id = '{id}';";
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