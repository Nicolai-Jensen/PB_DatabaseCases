using DatabaseEksam.ORMClasses;
using Npgsql;

namespace DatabaseEksam.TestStuff;

public class PlayerService
{
    private readonly NpgsqlDataSource _dataSource;
    
    public PlayerService(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    public int CreatePlayer(string username, string password)
    {
        using var connection = _dataSource.OpenConnection();
        
        const string sql = @"
            INSERT INTO players (username, password) 
            VALUES (@username, @password) 
            RETURNING player_id";
        
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@password", password);
        
        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }
    
    public int CreatePlayerWithOrm(string username, string password)
    {
        using var context = new DatabaseContext();

        var player = new Player
        {
            Username = username,
            Password = password
        };

        context.Players.Add(player);
        context.SaveChanges();

        return player.PlayerId; // This is auto-populated after SaveChanges()
    }
    
    public PlayerData? GetPlayerById(int playerId)
    {
        using var connection = _dataSource.OpenConnection();
        
        const string sql = @"
            SELECT player_id, username, password 
            FROM players 
            WHERE player_id = @playerId";
        
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@playerId", playerId);
        
        using var reader = command.ExecuteReader();
        
        if (reader.Read())
        {
            return new PlayerData
            {
                PlayerId = reader.GetInt32(0),
                Username = reader.GetString(1),
                Password = reader.GetString(2)
            };
        }
        
        return null;
    }
}