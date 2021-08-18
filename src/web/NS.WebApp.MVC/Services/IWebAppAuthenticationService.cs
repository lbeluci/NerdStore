using NS.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public interface IWebAppAuthenticationService
    {
        Task<LoginUserResponse> Login(LoginUser loginUser);

        Task<LoginUserResponse> SignIn(RegistrationUser registrationUser);
    }
}