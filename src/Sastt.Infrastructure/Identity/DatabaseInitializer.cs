using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Sastt.Domain.Identity;

namespace Sastt.Infrastructure.Identity
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            var roles = new[]
            {
                SasttRoles.Admin,
                SasttRoles.Planner,
                SasttRoles.Technician,
                SasttRoles.TrainingOfficer,
                SasttRoles.Auditor,
                SasttRoles.Viewer
            };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@sastt.local";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                await _userManager.CreateAsync(adminUser, "Passw0rd!");
            }

            await _userManager.AddToRolesAsync(adminUser, roles);
        }
    }
}
