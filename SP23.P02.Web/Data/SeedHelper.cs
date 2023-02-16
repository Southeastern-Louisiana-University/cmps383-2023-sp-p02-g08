using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.TrainStations;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Data;
public static class SeedHelper
{

    public static async Task MigrateAndSeed(IServiceProvider serviceProvider)
    {
        var dataContext = serviceProvider.GetRequiredService<DataContext>();

        await dataContext.Database.MigrateAsync();

        await CreateRoles(serviceProvider);
        await CreateUsers(serviceProvider);
        await CreateTrainStations(dataContext);
    }
    public static async Task CreateRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

        if (await roleManager.Roles.AnyAsync())
        {
            return;
        }

        await roleManager.CreateAsync(new Role
        {
            Name = RoleNames.Admin,
        });
        await roleManager.CreateAsync(new Role
        {
            Name = RoleNames.User,
        });

        await serviceProvider.GetRequiredService<DataContext>().SaveChangesAsync();
    }

    public static async Task CreateUsers(IServiceProvider serviceProvider)
    {
        const string password = "Password123!";

        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        if (await userManager.Users.AnyAsync())
        {
            return;
        }

        var admin = new User
        {
            UserName = "galkadi",
        };

        await userManager.CreateAsync(admin, password);
        await userManager.AddToRoleAsync(admin, RoleNames.Admin);

        var bob = new User
        {
            UserName = "bob"
        };

        await userManager.CreateAsync(bob, password);
        await userManager.AddToRoleAsync(bob, RoleNames.User);

        var sue = new User
        {
            UserName = "sue"
        };

        await userManager.CreateAsync(sue, password);
        await userManager.AddToRoleAsync(sue, RoleNames.User);

        await serviceProvider.GetRequiredService<DataContext>().SaveChangesAsync();
    }

    public static async Task CreateTrainStations(DataContext dataContext)
    {
        var trainStations = dataContext.Set<TrainStation>();

        if (await trainStations.AnyAsync())
        {
            return;
        }

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