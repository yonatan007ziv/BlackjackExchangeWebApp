using Microsoft.Data.SqlClient;

namespace FirstWeb
{
    internal static class DatabaseHandler
    {
        private const string connString = @"";
        private readonly static SqlConnection conn = new SqlConnection(connString);

        public static bool UsernameExists(string username)
        {
            SqlParameter usernameParam = new SqlParameter("@Username", username);
            int result = Convert.ToInt32(ExecuteScalar("SELECT Count(Username) FROM [UserInfo] WHERE Username = @Username", new SqlParameter[] { usernameParam }));
            return result > 0;
        }

        public static bool EmailExists(string email)
        {
            SqlParameter emailParam = new SqlParameter("@Email", email);
            int result = Convert.ToInt32(ExecuteScalar("SELECT Count(Email) FROM [UserInfo] WHERE Email = @Email", new SqlParameter[] { emailParam }));
            return result > 0;
        }

        public static bool PasswordCorrect(string username, string password)
        {
            SqlParameter usernameParam = new SqlParameter("@Username", username);
            SqlParameter passwordParam = new SqlParameter("@Password", password);
            int result = Convert.ToInt32(ExecuteScalar("SELECT Count(*) FROM [UserInfo] WHERE Username = @Username AND Password = @Password", new SqlParameter[] { usernameParam, passwordParam }));
            return result > 0;
        }

        public static void InsertUser(string username, string password, string email)
        {
            Console.WriteLine($"INSERTING USER: ({username}, {password}, {email})");
            SqlParameter usernameParam = new SqlParameter("@Username", username);
            SqlParameter passwordParam = new SqlParameter("@Password", password);
            SqlParameter emailParam = new SqlParameter("@email", email);
            ExecuteScalar("INSERT INTO [UserInfo] (Username, Password, Email, Balance) VALUES (@Username, @Password, @email, '1000')", new SqlParameter[] { usernameParam, passwordParam, emailParam });
        }

        private static object ExecuteScalar(string commandText, SqlParameter[]? parameters = null)
        {
            SqlCommand command = conn.CreateCommand();

            command.CommandText = commandText;
            if (parameters != null)
                command.Parameters.AddRange(parameters);

            conn.Open();
            object result = command.ExecuteScalar();
            conn.Close();
            command.Dispose();
            return result;
        }
    }
}