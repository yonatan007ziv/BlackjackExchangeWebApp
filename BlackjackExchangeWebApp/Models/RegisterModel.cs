using BlackjackExchangeWebApp.Models.AccountModels;

namespace BlackjackExchangeWebApp.Models
{
    public class RegisterModel
    {
        public RegisterUserModel UserModel { get; set; } = null!;
        public string? Response { get; set; }
    }
}