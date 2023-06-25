namespace FirstWeb
{
    internal class LoginRegisterCredentials
    {
        public string request { get; set; } = "";
        public string username { get; set; } = "";
        public string password { get; set; } = "";
        public string email { get; set; } = "";
        public int twoFA { get; set; } = 0;
    }
}