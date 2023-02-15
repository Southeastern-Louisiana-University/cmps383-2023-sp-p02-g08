using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.TrainStations;

namespace SP23.P02.Web.Data;

public static class SeedHelper
{
    public static async Task MigrateAndSeed(IServiceProvider serviceProvider)
    {
        var dataContext = serviceProvider.GetRequiredService<DataContext>();

        await dataContext.Database.MigrateAsync();

        await CreateRoles(serviceProvider);
        //await CreateUsers(serviceProvider);
        await dataContext.Database.MigrateAsync();

        var trainStations = dataContext.Set<TrainStation>();

        if (!await trainStations.AnyAsync())
        {
            for (int i = 0; i < 3; i++)
            {
                dataContext.Set<TrainStation>()
                    .Add(new TrainStation
                    {
                        Name = "Hammond",
                        Address = "1234 Place st"
                    });
            }

            await dataContext.SaveChangesAsync();
        }
        
    }
    public static async Task CreateRoles(IServiceProvider serviceProvider)
    {

        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new Role
            {
                Name = Role.Admin,
            });
            await roleManager.CreateAsync(new Role
            {
                Name = Role.User,
            });
        }
    }

    public static async Task CreateUsers(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        const string password = "password123!";
        if(!await userManager.Users.AnyAsync())
        {
            var admin = new User
            {
                UserName = "galkadi",
            };

            await userManager.CreateAsync(admin, password);
            await userManager.AddToRoleAsync(admin, password);

            var bob = new User
            {
                UserName = "bob"
            };

            await userManager.CreateAsync(bob, password);
            await userManager.AddToRoleAsync(bob, password);

            var sue = new User
            {
                UserName = "sue"
            };

            await userManager.CreateAsync(sue, password);
            await userManager.AddToRoleAsync(sue, password);


        }
    }
}