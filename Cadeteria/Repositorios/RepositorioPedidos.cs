using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Models;
using Microsoft.Data.Sqlite;

namespace Cadeteria.Repositorios
{
    public class RepositorioPedidos : IRepositorioPedidos
    {
        private string cadenaConexion = "Data Source=DB/PedidosDB.db;Cache=Shared";
        public List<Pedido> GetAll()
        {
            var queryString = $"SELECT * FROM Pedidos WHERE Activo = {1};";
            List<Pedido> pedidos = new List<Pedido>();
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
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
            }
            return pedidos;
        }

        public Pedido GetById(int idPedido)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteDataReader lector;
            var pedido = new Pedido();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Pedidos WHERE Nro = '{idPedido}';";
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    pedido.Nro = Convert.ToInt32(reader["Nro"]);
                    pedido.Obs = reader["Obs"].ToString();
                    pedido.ClienteID = Convert.ToInt32(reader["ClienteId"]);
                    pedido.Estado = reader["Estado"].ToString();
                    pedido.CadeteID = Convert.ToInt32(reader["CadeteId"]);
                }
            }
            connection.Close();

            return (pedido);
        }

        public void Create(Pedido pedido)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO Pedidos (Obs, ClienteId, Estado, CadeteId, Activo) VALUES ('{pedido.Obs}', '{pedido.ClienteID}', '{pedido.Estado}', '{pedido.CadeteID}', '{1}');";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Pedido pedido)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Pedidos SET Obs = '{pedido.Obs}', Estado = '{pedido.Estado}',  ClienteId = '{pedido.ClienteID}', CadeteId = '{pedido.CadeteID}' WHERE Nro = '{pedido.Nro}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(int id)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Pedidos SET Activo = {0} WHERE Nro = '{id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }
    }
}