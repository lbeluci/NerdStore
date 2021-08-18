using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IWebAppAuthenticationService _webAppAuthenticationService;

        public IdentityController(IWebAppAuthenticationService webAppAuthenticationService)
        {
            _webAppAuthenticationService = webAppAuthenticationService;
        }

        [HttpGet]
        [Route("registration")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration(RegistrationUser registrationUser)
        {
            if (!ModelState.IsValid)
            {
                return View(registrationUser);
            }

            var response = await _webAppAuthenticationService.SignIn(registrationUser);

            await DoLogin(response);

            //if (false)
            //{
            //    return View(registrationUser);
            //}

            //Login

            //if (false)
            //{
            //    return View(registrationUser);
            //}

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser loginUser, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(loginUser);
            }

            var response = await _webAppAuthenticationService.Login(loginUser);

            await DoLogin(response);

            //if (false)
            //{
            //    return View(loginUser);
            //}

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }

        private async Task DoLogin(LoginUserResponse response)
        {
            var token = GetFormattedToken(response.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", response.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties { ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60), IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        private static JwtSecurityToken GetFormattedToken(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
        }
    }
}