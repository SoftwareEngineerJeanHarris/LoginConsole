using MySql.Data.MySqlClient;
using System;

namespace MySQLConnectionExample
{
    class Program
    {
        static string connectionString = "Server=89.117.139.52;Port=3306;Database=u428290900_Jean;Uid=u428290900_Jean;Pwd=Jmjmjm_1993;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1: Login");
                Console.WriteLine("2: Create User");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        CreateUser();
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE username='{username}' AND password_hash='{password}'", connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Logged in successfully!");
                        LoggedInOptions(username);
                    }
                    else
                    {
                        Console.WriteLine("Login failed!");
                    }
                }
            }
        }

        static void LoggedInOptions(string username)
        {
            while (true)
            {
                Console.WriteLine("1: Logout");
                Console.WriteLine("2: Modify Username/Password");
                Console.WriteLine("3: Delete User");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Logged out successfully!");
                        return;
                    case "2":
                        ModifyUser(username);
                        break;
                    case "3":
                        DeleteUser(username);
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void ModifyUser(string username)
        {
            Console.Write("Enter new username (leave blank to keep current): ");
            string newUsername = Console.ReadLine();

            Console.Write("Enter new password (leave blank to keep current): ");
            string newPassword = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                if (!string.IsNullOrEmpty(newUsername))
                {
                    MySqlCommand cmd = new MySqlCommand($"UPDATE users SET username='{newUsername}' WHERE username='{username}'", connection);
                    cmd.ExecuteNonQuery();
                    username = newUsername; // Update the current username
                }

                if (!string.IsNullOrEmpty(newPassword))
                {
                    MySqlCommand cmd = new MySqlCommand($"UPDATE users SET password_hash='{newPassword}' WHERE username='{username}'", connection);
                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("User details updated successfully!");
            }
        }

        static void DeleteUser(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM users WHERE username='{username}'", connection);
                cmd.ExecuteNonQuery();
                Console.WriteLine("User deleted successfully!");
            }
        }

        static void CreateUser()
        {
            Console.Write("Enter new username: ");
            string username = Console.ReadLine();

            Console.Write("Enter new password: ");
            string password = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO users (username, password_hash) VALUES ('{username}', '{password}')", connection);
                cmd.ExecuteNonQuery();
                Console.WriteLine("User created successfully!");
            }
        }
    }
}
