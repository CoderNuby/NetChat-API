using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(UserManager<AppUser> userManager)
        {
            if(userManager.Users.Count() == 0)
            {
                var users = new List<AppUser>()
                {
                    new AppUser()
                    {
                        Id = "1",
                        UserName = "Rodrigo",
                        Email = "rodrigo@test.com"
                    },
                    new AppUser()
                    {
                        Id = "2",
                        UserName = "Carlos",
                        Email = "carlos@test.com"
                    },
                    new AppUser()
                    {
                        Id = "3",
                        UserName = "Tom",
                        Email = "tom@test.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "paS$w0rd");
                }
            }
        }
    }
}
