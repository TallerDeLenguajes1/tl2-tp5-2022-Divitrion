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
            var queryString = @"SELECT * FROM CADETES;";
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
                        pedido.Cliente = new Cliente();
                        pedido.Estado = reader["Estado"].ToString();
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
                    pedido.clienteID = Convert.ToInt32(reader["Cliente"]);
                    pedido.Estado = reader["Estado"].ToString();
                }
            }
            connection.Close();

            return (pedido);
        }

        public void Create(Pedido pedido)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO Pedidos (Obs, Estado, CadeteID, ClienteID) VALUES ('{pedido.Obs}', '{pedido.Estado}');";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Pedido pedido)
        {
            SqliteConnection connection = new SqliteConnection(cadenaConexion);
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Pedidos SET Obs = '{pedido.Obs}', Estado = '{pedido.Estado}',  ClienteID = '{pedido.Clienteid}', CadeteID = '{pedido.cadeteID}' WHERE Nro = '{pedido.Nro}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(int id)
        {

        }
    }
}