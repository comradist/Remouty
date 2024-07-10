
using System.Threading.Tasks;
using OutOfOffice.MVC.Models.Identity;
using OutOfOffice.MVC.Services.Base;

namespace OutOfOffice.MVC.Contracts
{
    public interface IAuthenticateService
    {
        Task<TokenVM> RefreshToken(TokenVM token);
        Task<TokenVM> Authenticate(UserAuthenticationVM userAuthenticationVM);
        Task<TokenVM> Register(UserRegistrationVM registration);
        Task Logout();
        void AddCookies(TokenVM tokenVM, in HttpResponse httpResponse);
        UserVM GetCurrentUser();
    }
}
