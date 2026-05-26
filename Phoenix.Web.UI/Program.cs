using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Phoenix.Web.UI {
    public class Program {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option => {
                    option.LoginPath = "/Account/Login";
                });

            builder.Services.AddAuthorization(options => {
                // Create policies
                options.AddPolicy("Guest", p => p.RequireRole("Guest"));
                options.AddPolicy("Administrator", p => p.RequireRole("Administrator"));
            });

            builder.Services.AddRazorPages(options => {

                // Set authorization for areas (looks like no way to do all areas at once)
                options.Conventions.AuthorizeAreaFolder("Admin", "/", "Administrator");
                options.Conventions.AuthorizeAreaFolder("Inventory", "/", "Administrator");
                options.Conventions.AuthorizeAreaFolder("ReservationLog", "/", "Administrator");
                options.Conventions.AuthorizeAreaFolder("Room", "/", "Administrator");
                options.Conventions.AuthorizeAreaFolder("RoomService", "/", "Administrator");
                options.Conventions.AuthorizeAreaFolder("Booking", "/", "Guest");

                // Anonymous pages
                options.Conventions.AllowAnonymousToFolder("/Home");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
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