
using System.Threading.Tasks;
using OutOfOffice.MVC.Models.Identity;
using OutOfOffice.MVC.Services.Base;

namespace OutOfOffice.MVC.Contracts
{
    public interface IAuthenticateService
    {
        Task<TokenVM> RefreshToken(TokenVM token);
        Task<TokenVM> Authenticate(string email, string password);
        Task Register(UserRegistrationVM registration);
        Task Logout();
    }
}
