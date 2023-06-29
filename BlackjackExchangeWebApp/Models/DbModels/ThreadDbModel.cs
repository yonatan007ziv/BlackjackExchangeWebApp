namespace BlackjackExchangeWebApp.Models.DbModels
{
    public class ThreadDbModel
    {
        public int Id { get; set; }
        public int UserId  { get; set; }
        public UserDbModel User { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
    }
}