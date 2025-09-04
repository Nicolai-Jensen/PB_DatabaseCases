using Npgsql;

namespace DatabaseEksam;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Console.WriteLine("Nicolai was Here");
        
        string connectionString = "Host=localhost;Username=postgres;Password=07N7HZpdByMgTdB2yqH8;Database=DatabaseCase2";
    
        try
        {
            NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);
        
            // Actually test the connection
            using (var connection = dataSource.OpenConnection())
            {
                Console.WriteLine("Successfully connected to PostgreSQL!");
                Console.WriteLine($"Database: {connection.Database}");
                Console.WriteLine($"Server Version: {connection.PostgreSqlVersion}");
            }
            
            // Test the player account methods
            Console.WriteLine("\n--- Testing Player Account Methods ---");
            CreateAndReadPlayerAccount(dataSource, "TestPlayer123", "Kiryu");
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"PostgreSQL Error: {ex.Message}");
            Console.WriteLine($"Error Code: {ex.SqlState}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
        }
    }
    
    static void CreateAndReadPlayerAccount(NpgsqlDataSource dataSource, string username, string password)
    {
        try
        {
            // Step 1: Insert the new player
            int newPlayerId = InsertPlayer(dataSource, username, password);
            Console.WriteLine($"Successfully created player with ID: {newPlayerId}");
            
            // Step 2: Read back the player data
            var playerData = GetPlayerById(dataSource, newPlayerId);
            
            if (playerData != null)
            {
                Console.WriteLine("Player data retrieved:");
                Console.WriteLine($"   Player ID: {playerData.PlayerId}");
                Console.WriteLine($"   Username: {playerData.Username}");
                Console.WriteLine($"   Password: {playerData.Password}");
            }
            else
            {
                Console.WriteLine(" Failed to retrieve player data");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CreateAndReadPlayerAccount: {ex.Message}");
        }
    }
    
    static int InsertPlayer(NpgsqlDataSource dataSource, string username, string password)
    {
        using var connection = dataSource.OpenConnection();
        
        // Using parameterized query to prevent SQL injection
        // RETURNING clause gets us the auto-generated player_id
        const string sql = @"
            INSERT INTO players (username, password) 
            VALUES (@username, @password) 
            RETURNING player_id";
        
        using var command = new NpgsqlCommand(sql, connection);
        
        // Add parameters - this prevents SQL injection
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@password", password);
        
        // Execute and get the returned player_id
        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }
    
    
    static PlayerData? GetPlayerById(NpgsqlDataSource dataSource, int playerId)
    {
        using var connection = dataSource.OpenConnection();
        
        // Using parameterized query to prevent SQL injection
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
        
        return null; // Player not found
    }
    
    
    
    
}

public class PlayerData
{
    public int PlayerId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}