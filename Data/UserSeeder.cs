using DevJobPortal.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevJobPortal.Data
{
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            await CreateUserWithRole(userManager, "admin@devjobportal.com", "Admin123!", Roles.Admin);
            await CreateUserWithRole(userManager, "jobseeker@devjobportal.com", "JobSeeker123!", Roles.JobSeeker);
            await CreateUserWithRole(userManager, "employer@devjobportal.com", "Employer123!", Roles.Employer);
        }

        private static async Task CreateUserWithRole(
            UserManager<IdentityUser> userManager,
            string email,
            string password,
            string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin);
                }
                else
                {
                    throw new Exception($"Failed creating user with email {user.Email}. Errors:{string.Join(",", result.Errors)}");
                }

            }
        }
    }
}
