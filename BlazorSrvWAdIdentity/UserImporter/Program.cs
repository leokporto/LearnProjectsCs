using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserImporter;
using BlazorSrvWAdIdentity.Data;


namespace UserImporter
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var adService = services.GetRequiredService<ActiveDirectoryService>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            // Criar roles se não existirem
            await CheckRoles(roleManager);
            await SyncAdUsers(adService, userManager);

        }

        private static async Task SyncAdUsers(ActiveDirectoryService adService, UserManager<ApplicationUser> userManager)
        {
            var users = adService.GetActiveUsers();

            foreach (var aduser in users)
            {
                var fullUserName = "SPINENGENHARIA\\" + aduser.Username;
                var user = await userManager.FindByNameAsync(fullUserName);

                if (user == null)
                {
                    user = new ApplicationUser { UserName = fullUserName,  Email = aduser.Email };
                    
                    var result = await userManager.CreateAsync((ApplicationUser)user);

                    if (result.Succeeded)
                    {
                        Console.WriteLine($"Usuário {fullUserName} criado.");
                        await userManager.AddToRoleAsync(user, aduser.Role);
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao criar {fullUserName}: {string.Join(", ", result.Errors)}");
                    }
                }
            }
        }

        private static async Task CheckRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Administrador", "Usuario", "Licenciador" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    Console.WriteLine($"Role {role} criada.");
                }
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddDbContext<AppIdentityDbContext>(options =>
                        options.UseSqlite("Data Source=C:\\Temp\\TestIdentity.db"));

                    services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddEntityFrameworkStores<AppIdentityDbContext>();

                    services.AddSingleton(new ActiveDirectoryService("SPINENGENHARIA"));
                });

            return host;
        }
    }


    
}
