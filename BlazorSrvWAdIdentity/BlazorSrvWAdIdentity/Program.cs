using BlazorSrvWAdIdentity.Auth;
using BlazorSrvWAdIdentity.Biz;
using BlazorSrvWAdIdentity.Components;
using BlazorSrvWAdIdentity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace BlazorSrvWAdIdentity;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        //builder.Services.AddDbContext<AppIdentityDbContext>(options =>
        //{
        //    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        //});

        //builder.Services.AddDefaultIdentity<ApplicationUser>()
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<AppIdentityDbContext>()
        //    .AddDefaultTokenProviders();

        

        //builder.Services.Configure<IdentityOptions>(options =>
        //{
        //    options.User.RequireUniqueEmail = false;
        //    options.User.AllowedUserNameCharacters += @"\";
        //    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
        //    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
        //    options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
        //});

        builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate(); // Permite login via Windows Authentication

        builder.Services.AddCascadingAuthenticationState(); // Permite que o Blazor acesse a autenticação
        
        builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();        
        //builder.Services.AddScoped<IClaimsTransformation, CustomClaimsTransformation>();
        //builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomUserClaimsPrincipalFactory>();

        builder.Services.AddAuthorization(options => { 
            options.FallbackPolicy = options.DefaultPolicy;
        });

        //builder.Services.AddScoped<AdUserManager>();

        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

       

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode();

        app.MapGet("/whois", (HttpContext httpContext) =>
        {
            return httpContext.User.Identity?.Name ?? "Usuário não autenticado";
        }).RequireAuthorization();

        //app.MapGet("/syncad", async (AdUserManager adUserManager) =>
        //{
        //    await adUserManager.SyncAdUsers();
        //    return "Sincronização concluída";
        //}).RequireAuthorization();

        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}
