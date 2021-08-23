using Microsoft.Extensions.Options;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public class WebAppAuthenticationService : Service, IWebAppAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public WebAppAuthenticationService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(appSettings.Value.UrlAuthentication);
        }

        public async Task<LoginUserResponse> Login(LoginUser loginUser)
        {
            return await IdentityPostAsync(loginUser, "/api/identity/login");
        }

        public async Task<LoginUserResponse> SignIn(RegistrationUser registrationUser)
        {
            return await IdentityPostAsync(registrationUser, "/api/identity/register");
        }

        private async Task<LoginUserResponse> IdentityPostAsync<T>(T content, string url)
        {
            var response = await _httpClient.PostAsync(url, GetContentString(content));

            if (!HandleErrorsResponse(response))
            {
                return new LoginUserResponse { ResponseResult = await DeserializeResponseMessageAsync<ResponseResult>(response) };
            }

            return await DeserializeResponseMessageAsync<LoginUserResponse>(response);
        }
    }
}