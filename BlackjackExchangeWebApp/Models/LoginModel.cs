using BlackjackExchangeWebApp.Models.AccountModels;

namespace BlackjackExchangeWebApp.Models
{
    public class LoginModel
    {
        public LoginUserModel UserModel { get; set; } = null!;
        public string? Response { get; set; }
    }
}
