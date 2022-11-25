
using Cadeteria.Models;
using Microsoft.Data.Sqlite;

public class RepositorioCadetes
{
    public RepositorioCadetes()
    {
    }

    public List<Cadete> GetAll()
    {
        var connectionString = "Data Source=DB/PedidosDB.db;Cache=Shared"; 
        var queryString = @"SELECT * FROM CADETES;";
        List<Cadete> cadetes = new List<Cadete>();
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(queryString, connection);
            connection.Open();
           
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var cadete = new Cadete();
                    cadete.Id = Convert.ToInt32(reader["Id"]);
                    cadete.Id = Convert.ToInt32(reader["Id"]);
                    cadete.Id = Convert.ToInt32(reader["Id"]);
                    cadete.Id = Convert.ToInt32(reader["Id"]);
                    cadete.Id = Convert.ToInt32(reader["Id"]); 
                    cadetes.Add(cadete);
                }
            }
    
            connection.Close();
        }
        return cadetes;
    }
} 
