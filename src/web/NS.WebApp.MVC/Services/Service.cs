using NS.WebApp.MVC.Extensions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected bool HandleErrorsResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 400:
                    return false;
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected StringContent GetContentString<T>(T content)
        {
            return new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        }

        protected async Task<T> DeserializeResponseMessageAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}