using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

using BlazorSrvWAdIdentity.Auth;
using BlazorSrvWAdIdentity.Biz;
using BlazorSrvWAdIdentity.Components;
using BlazorSrvWAdIdentity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.Extensions.Options;

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



        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = $"https://login.microsoftonline.com/{builder.Configuration["AzureAd:TenantId"]}";
                options.ClientId = builder.Configuration["AzureAd:ClientId"];
                options.ClientSecret = builder.Configuration["AzureAd:ClientSecret"];
                options.ResponseType = "code"; // Utiliza o fluxo Authorization Code

                // Escopos solicitados
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");

                // Salvar tokens para uso posterior, se necessário
                options.SaveTokens = true;

                // Tratamento de erro na autenticação
                options.Events.OnAuthenticationFailed = context =>
                {
                    context.HandleResponse();
                    context.Response.Redirect("/erro-autenticacao");
                    return Task.CompletedTask;
                };
            }); // Permite login via Windows Authentication

        builder.Services.AddCascadingAuthenticationState(); // Permite que o Blazor acesse a autenticação

        builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

        builder.Services.AddAuthorization();



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
            string response = $"Tenant: {builder.Configuration["AzureAd:TenantId"]}\n";
            response += $"Client Id: {builder.Configuration["AzureAd:ClientId"]}\n";
            response += $"Client Secret: {builder.Configuration["AzureAd:ClientSecret"]}\n";
            
            return response;
        });


        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}
