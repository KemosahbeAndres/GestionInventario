using GestionInventarioWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using NuGet.Protocol;
using Microsoft.AspNetCore.Identity;

namespace GestionInventarioWeb.Controllers
{
    public class LoginController : Controller
    {

        private readonly GestionInventarioContext _context;

        public LoginController(GestionInventarioContext context)
        {
            _context = context;
        }

        [HttpGet("/Login", Name = "Login")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("Views/LoginView.cshtml");
        }

        [HttpPost("/Login", Name = "TryLogin")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string username, string password, bool rememberme)
        {
            //Response.Cookies.Append("token", "bbbb");
            //HttpContext.Session.Set("token", "");
            //var user = await AuthenticateUser(username, password);
            var user = _context.Usuarios.FirstOrDefault(u => u.Rut.Equals(username.Trim()) );

             if ( user == null || !user.Clave.Equals(password.Trim()) )
            {
                HttpContext.Session.SetString("ErrorMessage", "El usuario no ha sido encontrado o contraseña incorrecta!");
                LocalRedirect("/Login");
            }

             var role = _context.Roles.FirstOrDefault(r => r.Id.Equals(user.IdRol));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim("Rut", user.Rut),
                new Claim(ClaimTypes.Role, role.Rol)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = rememberme,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                IssuedUtc = DateTimeOffset.Now.AddHours(2),
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            HttpContext.Session.SetString("LoginMessage", "Sesion iniciada con exito! Bienvenido " + user.Nombre);

            return LocalRedirect("/Dashboard");
        }

        [HttpGet("/Logout", Name = "Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            HttpContext.Session.SetString("LoginMessage", "Sesion cerrada con exito!");

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }
    }
}
