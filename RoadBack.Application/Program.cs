using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoadBack.DAL;
using System.Security.Claims;

namespace Denunciation.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseInMemoryDatabase("MEMORY");
            }).AddIdentity<ApplicationUser,ApplicationRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength = 6;
            })
              .AddEntityFrameworkStores<ApplicationDbContext>();

            //builder.Services.AddAuthentication("Cookie")
            //    .AddCookie("Cookie", config =>
            //    {
            //        config.LoginPath = "/Admin/Login";
            //        config.AccessDeniedPath = "/Home/AccessDenied";
            //    });

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Admin/Login";
                config.AccessDeniedPath = "/Home/AccessDenied";
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Administrator");
                });

                options.AddPolicy("Manager", builder =>
                {
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Manager") 
                    || x.User.HasClaim(ClaimTypes.Role, "Administrator"));
                });

            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                DatabaseInitializer.Init(scope.ServiceProvider);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        public static class DatabaseInitializer
        {
            public static void Init(IServiceProvider scopeServiceProvider)
            {
                var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();

                var user = new ApplicationUser()
                {
                    UserName = "User",
                    LastName = "LastName",
                    FirstName = "FirstName"
                };

                var result = userManager.CreateAsync(user, "123qwe").GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
                }
            }
        }
    }
}
