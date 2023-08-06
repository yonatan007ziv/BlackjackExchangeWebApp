using BlackjackExchangeWebApp.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlackjackExchangeWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public DbSet<UserDbModel> Users { get; set; } = null!;
        public DbSet<ThreadDbModel> Threads { get; set; } = null!;

        public ApplicationDbContext(IConfiguration config, ILogger logger)
        { _config = config; _logger = logger; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThreadDbModel>()
                .HasOne<UserDbModel>(s => s.User)
                .WithMany(g => g.Threads)
                .HasForeignKey(s => s.UserId);
        }
    }
}