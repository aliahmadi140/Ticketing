using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Ticketing.Server.Seeds
{
    public static class RoleSeed
    {
        public static async Task SeedAsync(
        RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

        }
    }
}
