namespace BlackjackExchangeWebApp.Models.AccountModels
{
    public class RegisterUserModel
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}