using System.Data.SqlClient;
using System.Data.SQLite;

namespace FirstWeb.Database
{
    internal static class DatabaseHandler
    {
        private const string connString = @"Data Source=|DataDirectory|\UserInfo.db;Version=3;";
        private readonly static SQLiteConnection conn = new SQLiteConnection(connString);

        public static bool UsernameExists(string username)
        {
            SQLiteParameter usernameParam = new SQLiteParameter("@Username", username);
            int result = Convert.ToInt32(ExecuteScalar("SELECT Count(Username) FROM [UserInfo] WHERE Username = @Username", new SQLiteParameter[] { usernameParam }));
            return result > 0;
        }

        public static bool EmailExists(string email)
        {
            SQLiteParameter emailParam = new SQLiteParameter("@Email", email);
            int result = Convert.ToInt32(ExecuteScalar("SELECT Count(Email) FROM [UserInfo] WHERE Email = @Email", new SQLiteParameter[] { emailParam }));
            return result > 0;
        }

        public static bool PasswordCorrect(string username, string password)
        {
            SQLiteParameter usernameParam = new SQLiteParameter("@Username", username);
            SQLiteParameter passwordParam = new SQLiteParameter("@Password", password);
            int result = Convert.ToInt32(ExecuteScalar("SELECT Count(*) FROM [UserInfo] WHERE Username = @Username AND Password = @Password", new SQLiteParameter[] { usernameParam, passwordParam }));
            return result > 0;
        }

        public static void InsertUser(string username, string password, string email)
        {
            Console.WriteLine($"INSERTING USER: ({username}, {password}, {email})");
            SQLiteParameter usernameParam = new SQLiteParameter("@Username", username);
            SQLiteParameter passwordParam = new SQLiteParameter("@Password", password);
            SQLiteParameter emailParam = new SQLiteParameter("@email", email);
            ExecuteScalar("INSERT INTO [UserInfo] (Username, Password, Email, Balance) VALUES (@Username, @Password, @email, '1000')", new SQLiteParameter[] { usernameParam, passwordParam, emailParam });
        }

        private static object ExecuteScalar(string commandText, SQLiteParameter[]? parameters = null)
        {
            SQLiteCommand command = conn.CreateCommand();

            command.CommandText = commandText;
            command.Parameters.AddRange(parameters);
            conn.Open();
            object result = command.ExecuteScalar();
            conn.Close();
            command.Dispose();
            return result;
        }
    }
}