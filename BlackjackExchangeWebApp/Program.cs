using BlackjackExchangeWebApp.Data;
using BlackjackExchangeWebApp.Interfaces;
using BlackjackExchangeWebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

            #region Custom Services

            // AUTHENTICATION
            builder.Services.AddAuthentication(options => 
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            // AUTHORIZATION
            //builder.Services.AddAuthorization();

            builder.Services.AddSingleton<ILogger, ConsoleLogger>();

            // DB
            string connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IDatabaseService, DbService>();

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}