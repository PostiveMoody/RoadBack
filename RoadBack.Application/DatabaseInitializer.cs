using Microsoft.AspNetCore.Identity;
using RoadBack.DAL;
using System.Security.Claims;

namespace Denunciation.Application
{

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
