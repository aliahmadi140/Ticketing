using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Ticketing.Server.Models;

namespace Ticketing.Server.Seeds
{
    public static class AdminUserSeed
    {
        public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager)
        {

            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "AdminUser",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Email="ahmadiali709@gmail.com"
            };
           
            var user = await userManager.FindByNameAsync(defaultUser.UserName);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Admin");
                await userManager.AddToRoleAsync(defaultUser, "Admin");
            }

          
        }
    }
}
