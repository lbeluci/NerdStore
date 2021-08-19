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
    public class IdentityController : MainController
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

            if (HasErrors(response.ResponseResult))
            {
                return View(registrationUser);
            }

            await DoLogin(response);

            return RedirectToAction("Index", "Home");
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

            if (HasErrors(response.ResponseResult))
            {
                return View(loginUser);
            }

            await DoLogin(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
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