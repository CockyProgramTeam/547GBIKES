using System;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Npgsql;
using System;


namespace T4PGSQLINSTALLER
{

public class PGSQLINSTALLER
    {

    public static void CreateDatabase()
    {
        string connectionString = "Host=10.144.0.100;Port=5434;Username=postgres;Password=yourpassword;Database=postgres";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Your Current Connection String is: " + connectionString);

            try
            {
                // Check if the database exists
                using (var checkCommand = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = 'gem';", connection))
                {
                    var exists = checkCommand.ExecuteScalar();
                    if (exists != null)
                    {
                        Console.WriteLine("Database 'gem' already exists.");
                        return;
                    }
                }

                // Create database
                using (var createCommand = new NpgsqlCommand("CREATE DATABASE gem;", connection))
                {
                    createCommand.ExecuteNonQuery();
                    Console.WriteLine("Database 'gem' created successfully.");
                }

                // Reconnect to the new database
                connection.ChangeDatabase("gem");

                // Create a new user and grant privileges
                string createUserQuery = @"
                DO $$ 
                BEGIN
                    IF NOT EXISTS (SELECT FROM pg_roles WHERE rolname = 'cm_user') THEN
                        CREATE ROLE cm_user LOGIN PASSWORD 'cm_password';
                    END IF;
                END $$;
                
                GRANT ALL PRIVILEGES ON DATABASE gem TO cm_user;";
                
                using (var userCommand = new NpgsqlCommand(createUserQuery, connection))
                {
                    userCommand.ExecuteNonQuery();
                    Console.WriteLine("User 'cm_user' created and granted privileges successfully.");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

        }
    }
}
    
