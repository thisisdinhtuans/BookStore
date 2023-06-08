using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public static class UserRoleInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<DefaultUser>>();

            string[] roleNames = { "Admin", "User" };

            IdentityResult roleResult;
            foreach(var role in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if(!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(role: new IdentityRole(role));
                }
            }
            var email = "admin@site.com";
            var password = "Qwerty123!";

            if(userManager.FindByEmailAsync(email).Result==null)
            {
                DefaultUser user = new()
                {
                    Email = email,
                    UserName = email,
                    FirstName = "Admin",
                    LastName = "Adminsson",
                    Address = "Addstreet 3",
                    City = "Big City",
                    ZipCode = "12345"
                };
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, role: "Admin").Wait();
                }
            }
        }

    }
}
