using Microsoft.EntityFrameworkCore;
using RoadBack.DAL;
using System.Security.Claims;

namespace Denunciation.Application
{
    public partial class Program
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
    }
}
