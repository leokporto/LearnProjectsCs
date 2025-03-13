using BlazorSrvWAdIdentity.Biz;
using BlazorSrvWAdIdentity.Components;
using BlazorSrvWAdIdentity.Data;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        builder.Services.AddDbContext<AppIdentityDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = false;
            options.User.AllowedUserNameCharacters += @"\";
        });

        builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate(); // Permite login via Windows Authentication

        builder.Services.AddCascadingAuthenticationState(); // Permite que o Blazor acesse a autenticação
        builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();


        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy; // Garante que todas as páginas exigem autenticação
        });

        builder.Services.AddScoped<AdUserManager>();

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


        app.Run();
    }
}
