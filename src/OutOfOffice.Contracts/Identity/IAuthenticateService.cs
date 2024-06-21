using Microsoft.AspNetCore.Identity;
using OutOfOffice.Shared.DTOs.Identity;

namespace OutOfOffice.Contracts.Identity;

public interface IAuthenticateService
{
    Task<IdentityResult> RegisterUser(UserRegistrationDto userAuthenticationDto);
    Task<bool> ValidateUser(UserAuthenticationDto userAuthenticationDto);
    Task<TokenDto> CreateToken(bool populateExp);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
}