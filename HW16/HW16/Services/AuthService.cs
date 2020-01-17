using HW16.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HW16.Services
{
    public class AuthService
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly DataContext context;

        public AuthService(IHttpContextAccessor httpContextAccessor, DataContext dataContext)
        {
            httpContext = httpContextAccessor;
            context = dataContext;
        }

        public async Task<bool> AuthenticateUser(string login, string password)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Login == login && x.Password == password);

            if (user is null) return false;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                RedirectUri = "/Admin/Portfolios/Index"
            };

            await httpContext.HttpContext.SignInAsync(
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);


            return true;
        }
        public async void SignOutUser()
        {
            await httpContext.HttpContext.SignOutAsync(
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
