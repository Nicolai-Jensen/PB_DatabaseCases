using DatabaseEksam.ORMClasses;
using Npgsql;

namespace DatabaseEksam.TestStuff;

public class PlayerAccountTest
{
    public void RunTests(string connectionString)
    {
        Console.WriteLine("\n--- Testing Player Account Methods ---");
        
        try
        {
            NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);
            var playerService = new PlayerService(dataSource);
            
            // Test creating and reading a player
            TestCreateAndReadPlayer(playerService, "TestPlayer123", "Kiryu");
            
            int id = playerService.CreatePlayerWithOrm(new Player {Username = "TestPlayerORM", Password = "Kiryu"});
            Console.WriteLine($"Player {id} created with ORM");
            // You can add more tests here
            // TestUpdatePlayer(playerService);
            // TestDeletePlayer(playerService);
            
            Console.WriteLine("All player tests completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
        }
    }
    
    private void TestCreateAndReadPlayer(PlayerService playerService, string username, string password)
    {
        try
        {
            // Step 1: Insert the new player
            int newPlayerId = playerService.CreatePlayer(username, password);
            Console.WriteLine($"Successfully created player with ID: {newPlayerId}");
            
            // Step 2: Read back the player data
            var playerData = playerService.GetPlayerById(newPlayerId);
            
            if (playerData != null)
            {
                Console.WriteLine("Player data retrieved:");
                Console.WriteLine($"   Player ID: {playerData.PlayerId}");
                Console.WriteLine($"   Username: {playerData.Username}");
                Console.WriteLine($"   Password: {playerData.Password}");
            }
            else
            {
                Console.WriteLine("Failed to retrieve player data");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in TestCreateAndReadPlayer: {ex.Message}");
        }
    }
}