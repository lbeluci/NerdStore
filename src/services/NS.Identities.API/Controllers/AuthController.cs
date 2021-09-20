using EasyNetQ;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NS.Core.Messages.Integration;
using NS.Identities.API.Models;
using NS.WebApi.Core.Controllers;
using NS.WebApi.Core.Identities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NS.Identities.API.Controllers
{
    [Route("api/identity")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        private IBus _bus;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegistrationUser registrationUser)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var user = new IdentityUser
            {
                UserName = registrationUser.Email,
                Email = registrationUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registrationUser.Password);

            if (result.Succeeded)
            {
                var registerCustomerResult = await RegisterCustomer(registrationUser);

                return CustomResponse(await GetLoginResponseAsync(user.Email));
            }

            result.Errors.ToList().ForEach((e) => AddError(e.Description));

            return CustomResponse();
        }

        private async Task<ResponseMessage> RegisterCustomer(RegistrationUser registrationUser)
        {
            var user = await _userManager.FindByEmailAsync(registrationUser.Email);

            var createdUserIntegrationEvent = new CreatedUserIntegrationEvent(Guid.Parse(user.Id), registrationUser.Name, registrationUser.Email, registrationUser.Cpf);

            _bus = RabbitHutch.CreateBus("host=localhost:5672");

            var success = await _bus.Rpc.RequestAsync<CreatedUserIntegrationEvent, ResponseMessage>(createdUserIntegrationEvent);

            return success;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GetLoginResponseAsync(loginUser.Email));
            }

            if (result.IsLockedOut)
            {
                AddError("User temporarily blocked due to excessive invalid attempts.");
                return CustomResponse();
            }

            AddError("Username or password is invalid.");
            return CustomResponse();
        }

        private async Task<LoginResponse> GetLoginResponseAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await GetUserClaims(user, claims);

            var encodedToken = EncodeToken(identityClaims);

            return GetLoginResponseToken(encodedToken, user, claims);
        }

        private async Task<ClaimsIdentity> GetUserClaims(IdentityUser user, IList<Claim> claims)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);

            return claimsIdentity;
        }

        private string EncodeToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emitter,
                Audience = _appSettings.ValidIn,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private LoginResponse GetLoginResponseToken(string encodedToken, IdentityUser user, IList<Claim> claims)
        {
            return new LoginResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationInHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }
}