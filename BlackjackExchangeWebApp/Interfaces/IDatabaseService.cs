namespace BlackjackExchangeWebApp.Interfaces
{
    public interface IDatabaseService
    {
        void InsertUser(string username, string password, string email);
        bool UsernameExists(string username);
        bool UsernamePasswordPairExists(string username, string password);
        bool EmailExists(string email);
        List<Models.DbModels.ThreadDbModel> GetThreadsList();
    }
}
