namespace BlackjackExchangeWebApp.Models.DbModels
{
    public class UserDbModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<ThreadDbModel> Threads { get; set; } = new List<ThreadDbModel>();
    }
}