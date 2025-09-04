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
}