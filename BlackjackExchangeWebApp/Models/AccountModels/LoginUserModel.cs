namespace BlackjackExchangeWebApp.Models.AccountModels
{
    public class LoginUserModel
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public override string ToString()
        {
            return $"{Username}:{Password}";
        }
    }
}
