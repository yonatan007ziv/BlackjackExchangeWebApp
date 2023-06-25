using System.Net;
using System.Text.Json;
using FirstWeb.Database;

namespace FirstWeb
{
    enum LoginRegisterResult
    {
        Success,
        Error,
        UsernameDoesNotExist,
        WrongPassword,
        UsernameExists,
        InvalidPassword,
        EmailExists,
        InvalidEmail,
        TwoFANeeded,
        Wrong2FA,
    }

    class LoginRegisterHandler
    {
        private static readonly Dictionary<Client, LoginRegisterHandler> currentOperations = new Dictionary<Client, LoginRegisterHandler>();
        private static readonly Random random = new Random();

        private int current2FA;

        private LoginRegisterHandler(Client operatingClient, HttpListenerContext currentContext, string jsonRepresentation)
        {
            currentOperations.Add(operatingClient, this);

            LoginRegisterCredentials credentials = JsonSerializer.Deserialize<LoginRegisterCredentials>(jsonRepresentation) ?? new LoginRegisterCredentials();
            LoginRegisterResult result = ValidateRequest(credentials);
            operatingClient.SendBodyMessage(result.ToString(), currentContext);

            if (result != LoginRegisterResult.TwoFANeeded)
                currentOperations.Remove(operatingClient);

            if (credentials.request == "Register" && result == LoginRegisterResult.Success)
                DatabaseHandler.InsertUser(credentials.username, credentials.password, credentials.email);
        }

        public static void RouteToAppropriateRequest(Client operatingClient, HttpListenerContext currentContext, string jsonRepresentation)
        {
            if (currentOperations.ContainsKey(operatingClient))
            {
                LoginRegisterCredentials credentials = JsonSerializer.Deserialize<LoginRegisterCredentials>(jsonRepresentation) ?? new LoginRegisterCredentials();
                LoginRegisterResult result = currentOperations[operatingClient].Validate2FACode(credentials, currentContext);
                operatingClient.SendBodyMessage(result.ToString(), currentContext);
                currentOperations.Remove(operatingClient);

                if (result == LoginRegisterResult.Success)
                    DatabaseHandler.InsertUser(credentials.username, credentials.password, credentials.email);
            }
            else
                _ = new LoginRegisterHandler(operatingClient, currentContext, jsonRepresentation);
        }

        public LoginRegisterResult Validate2FACode(LoginRegisterCredentials credentials, HttpListenerContext currentContext)
        {
            return current2FA == credentials.twoFA ? LoginRegisterResult.Success : LoginRegisterResult.Wrong2FA;
        }
        public LoginRegisterResult ValidateRequest(LoginRegisterCredentials credentials)
        {
            if (credentials == null)
                return LoginRegisterResult.Error;

            if (credentials.request == "Login")
            {
                if (!DatabaseHandler.UsernameExists(credentials.username))
                    return LoginRegisterResult.UsernameDoesNotExist;
                if (!DatabaseHandler.PasswordCorrect(credentials.username, credentials.password))
                    return LoginRegisterResult.WrongPassword;

                return LoginRegisterResult.Success;
            }
            else if (credentials.request == "Register")
            {
                if (DatabaseHandler.UsernameExists(credentials.username))
                    return LoginRegisterResult.UsernameExists;
                if (string.IsNullOrEmpty(credentials.password))
                    return LoginRegisterResult.InvalidPassword;
                if (DatabaseHandler.EmailExists(credentials.email))
                    return LoginRegisterResult.EmailExists;

                current2FA = random.Next(1000, 10000);
                if (!SMTPHandler.SendEmail(credentials.email, "2FA", $"Code Is: {current2FA}"))
                    return LoginRegisterResult.InvalidEmail;

                return LoginRegisterResult.TwoFANeeded;
            }

            return LoginRegisterResult.Error;
        }
    }
}