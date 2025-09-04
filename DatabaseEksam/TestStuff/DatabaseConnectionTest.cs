using Npgsql;

namespace DatabaseEksam.TestStuff;

public class DatabaseConnectionTest
{
    public bool TestConnection(string connectionString)
    {
        try
        {
            NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);
            
            using (var connection = dataSource.OpenConnection())
            {
                Console.WriteLine($"Database: {connection.Database}");
                Console.WriteLine($"Server Version: {connection.PostgreSqlVersion}");
                return true;
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"PostgreSQL Error: {ex.Message}");
            Console.WriteLine($"Error Code: {ex.SqlState}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
            return false;
        }
    }
}