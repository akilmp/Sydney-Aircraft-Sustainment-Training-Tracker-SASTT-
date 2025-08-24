using Microsoft.AspNetCore.Identity;
using Sastt.Domain.Entities;
using Sastt.Domain.Identity;
using Sastt.Infrastructure.Identity;
using Sastt.Infrastructure.Persistence;

namespace Sastt.Infrastructure.SeedData;

public static class DataSeeder
{
    public static async Task SeedAsync(SasttDbContext context, UserManager<ApplicationUser> userManager, string baseCode)
    {
        switch (baseCode.ToUpperInvariant())
        {
            case "YSSY":
                await SeedBaseAsync(context, userManager, YssySeed.Aircraft, YssySeed.Pilots, YssySeed.Technicians);
                break;
            case "YWLM":
                await SeedBaseAsync(context, userManager, YwlmSeed.Aircraft, YwlmSeed.Pilots, YwlmSeed.Technicians);
                break;
            default:
                throw new ArgumentException("Base must be YSSY or YWLM", nameof(baseCode));
        }

        await context.SaveChangesAsync();
    }

    private static async Task SeedBaseAsync(
        SasttDbContext context,
        UserManager<ApplicationUser> userManager,
        (string TailNumber, string Model)[] aircraft,
        string[] pilots,
        string[] technicians)
    {
        foreach (var (tail, model) in aircraft)
        {
            if (!context.Aircraft.Any(a => a.TailNumber == tail))
            {
                context.Aircraft.Add(new Aircraft { TailNumber = tail, Model = model });
            }
        }

        foreach (var pilot in pilots)
        {
            if (!context.Pilots.Any(p => p.Name == pilot))
            {
                context.Pilots.Add(new Pilot { Name = pilot });
            }
        }

        foreach (var techEmail in technicians)
        {
            var user = await userManager.FindByEmailAsync(techEmail);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = techEmail,
                    Email = techEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "Passw0rd!");
                await userManager.AddToRoleAsync(user, SasttRoles.Technician);
            }
        }
    }
}

