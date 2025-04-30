using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wasted_Food.Data.Entities.Identity;


namespace WastedFood.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<Users> _userManager)
        {
            var usercount = await _userManager.Users.CountAsync();
            if (usercount <= 0)
            {
                var defaultUser = new Users
                {
                    UserName = "FutuerTeam",
                    Email = "ah6734735@gmail.com",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(defaultUser, "F123_f");
                await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
