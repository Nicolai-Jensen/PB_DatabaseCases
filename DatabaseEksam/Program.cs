using DatabaseEksam.TestStuff;
using Npgsql;

namespace DatabaseEksam;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Console.WriteLine("Nicolai was Here");

        string connectionString =
            "Host=localhost;Username=postgres;Password=07N7HZpdByMgTdB2yqH8;Database=DatabaseCase2";

        // Test database connection
        var connectionTest = new DatabaseConnectionTest();
        if (connectionTest.TestConnection(connectionString))
            
        {
            Console.WriteLine("Database connection successful!");

            // Run player account tests
            var playerTest = new PlayerAccountTest();
            playerTest.RunTests(connectionString);
        }
        else
        {
            Console.WriteLine("Database connection failed. Exiting...");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

}