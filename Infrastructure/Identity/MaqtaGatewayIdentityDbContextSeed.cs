using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class MaqtaGatewayIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Abhiroop",
                    Email = "Abhiroop@santra.com",
                    UserName = "Abhiroop@santra.com",
                    PhoneNumber = "+989107790912",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
