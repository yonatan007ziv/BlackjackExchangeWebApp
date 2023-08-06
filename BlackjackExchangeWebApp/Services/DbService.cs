using BlackjackExchangeWebApp.Data;
using BlackjackExchangeWebApp.Interfaces;
using BlackjackExchangeWebApp.Models.DbModels;

namespace BlackjackExchangeWebApp.Services
{
    public class DbService : IDatabaseService
    {
        private readonly ApplicationDbContext _context;

        public DbService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertUser(string username, string password, string email)
        {
            _context.Users.Add(new UserDbModel
            {
                Username = username,
                Password = password,
                Email = email,
            });
        }

        public bool UsernameExists(string username)
        {
            return _context.Users.Any(user => user.Username == username);
        }

        public bool UsernamePasswordPairExists(string username, string password)
        {
            return _context.Users.Any(user => user.Username == username && user.Password == password);
        }

        public bool EmailExists(string email)
        {
            return _context.Users.Any(user => user.Email == email);
        }

        public List<ThreadDbModel> GetThreadsList()
        {
            return _context.Threads.ToList();
        }
    }
}