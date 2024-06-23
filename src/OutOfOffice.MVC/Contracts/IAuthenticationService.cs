
using System.Threading.Tasks;
using OutOfOffice.Shared.DTOs.Identity;

namespace OutOfOffice.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> RefreshToken();
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(UserRegistrationVM registration);
        Task Logout();
    }
}
