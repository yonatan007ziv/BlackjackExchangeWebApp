using BlackjackExchangeWebApp.Data;
using BlackjackExchangeWebApp.Interfaces;
using BlackjackExchangeWebApp.Services;

namespace BlackjackExchangeWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Logging.ClearProviders();
            //builder.Logging.AddConsole();

            // Custom Services
            builder.Services.AddSingleton<ILogger, ConsoleLogger>();
            builder.Services.AddDbContext<ApplicationDbContext>();
            builder.Services.AddScoped<IDatabaseService, DbService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}