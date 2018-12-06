
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OcelotDemo.AuthAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotDemo.AuthAPI.Data
{
    public class ApplicationDbContextSeed
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

        public async Task SeedAsync(ApplicationDbContext context, int? retry = 0)
        {
            int retryForAvaiability = retry.Value;

            try
            {
                if (!context.Users.Any())
                {
                    context.Users.AddRange(GetDefaultUser());

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                if (retryForAvaiability < 10)
                {
                    retryForAvaiability++;


                    await SeedAsync(context, retryForAvaiability);
                }
            }
        }

        private IEnumerable<ApplicationUser> GetDefaultUser()
        {
            var user =
            new ApplicationUser()
            {
                Email = "erandika1986@gmail.com",
                Id = Guid.NewGuid().ToString(),
                PhoneNumber = "+94702605650",
                UserName = "erandika1986@gmail.com",
                NormalizedEmail = "ERANDIKA1986@GMAIL.COM",
                NormalizedUserName = "ERANDIKA1986@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, "Pass@word1");

            return new List<ApplicationUser>()
            {
                user
            };
        }
    }
}
