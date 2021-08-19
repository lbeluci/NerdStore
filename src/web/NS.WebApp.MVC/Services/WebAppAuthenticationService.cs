using NS.WebApp.MVC.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public class WebAppAuthenticationService : Service, IWebAppAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public WebAppAuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginUserResponse> Login(LoginUser loginUser)
        {
            return await DoSomething(loginUser, "https://localhost:44363/api/identity/login");
        }

        public async Task<LoginUserResponse> SignIn(RegistrationUser registrationUser)
        {
            return await DoSomething(registrationUser, "https://localhost:44363/api/identity/register");
        }

        private async Task<LoginUserResponse> DoSomething<T>(T value, string url) where T : class, new()
        {
            var loginContent = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, loginContent);

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (!HandleErrorsResponse(response))
            {
                return new LoginUserResponse { ResponseResult = JsonSerializer.Deserialize<ResponseResult>(content, options) };
            }

            return JsonSerializer.Deserialize<LoginUserResponse>(content, options);
        }
    }
}