using BlazorSrvWAdIdentity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSrvWAdIdentity.Biz
{
    public class AdUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdUserManager(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> EnsureUserExists(ClaimsPrincipal principal)
        {
            var username = principal.Identity?.Name; // "DOMINIO\\usuario"
            
            var email = $"{username.Split('\\').Last()}@spinengenharia.com.br"; // Exemplo de email baseado no login

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                user = new ApplicationUser { UserName = username };
                var result = await _userManager.CreateAsync(user);

            }

            return user;
        }
    }
}
