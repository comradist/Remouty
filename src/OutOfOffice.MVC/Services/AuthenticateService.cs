using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.MVC.Contracts;
using OutOfOffice.MVC.Models.Identity;
using OutOfOffice.MVC.Services.Base;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OutOfOffice.MVC.Services
{
    public class AuthenticateService : BaseHttpService, Contracts.IAuthenticateService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private JwtSecurityTokenHandler _tokenHandler;

        public AuthenticateService(IClient client, IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(client)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<TokenVM> Authenticate(UserAuthenticationVM userAuthenticationVM)
        {
            var userAuthenticationDto = _mapper.Map<UserAuthenticationDto>(userAuthenticationVM);
            var authenticationResponse = await _client.LoginAsync(userAuthenticationDto);

            if (authenticationResponse.Result.AccessToken == string.Empty && authenticationResponse.Result.RefreshToken == string.Empty)
            {
                throw new Exception("An error occurred while trying to authenticate the user.");    
            }

            var tokenAccessContent = _tokenHandler.ReadJwtToken(authenticationResponse.Result.AccessToken);
            var claims = ParseClaims(tokenAccessContent);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

            var tokenVm = _mapper.Map<TokenVM>(authenticationResponse.Result);

            tokenVm.AccessTokenExpires = tokenAccessContent.ValidTo;

            return tokenVm;
        }

        public async Task<TokenVM> Register(UserRegistrationVM registration)
        {

            UserRegistrationDto userRegistration = _mapper.Map<UserRegistrationDto>(registration);
            var authAPIResponse = await _client.RegisterAsync(userRegistration);

            if (authAPIResponse.StatusCode == StatusCodes.Status201Created)
            {
                UserAuthenticationVM userAuthenticationVM = new()
                {
                    UserNameOrEmail = registration.Email,
                    Password = registration.Password
                };
                var tokenVM = await Authenticate(userAuthenticationVM);
                return tokenVM;
            }
            else
            {
                throw new Exception("An error occurred while trying to register the user.");
            }
        }

        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            return claims;
        }

        public async Task<TokenVM> RefreshToken(TokenVM token)
        {
            var tokenDto = _mapper.Map<TokenDto>(token);
            var newToken = await _client.TokenAsync(tokenDto);
            if (newToken.Result.AccessToken == null || newToken.Result.RefreshToken == null)
            {
                throw new Exception("An error occurred while trying to refresh the token.");
            }

            var tokenVM = _mapper.Map<TokenVM>(newToken);

            return tokenVM;
        }

        public UserVM GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }
            var userVM = new UserVM
            {
                Id = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)),
                UserName = user.FindFirstValue(ClaimTypes.Name),
                Email = user.FindFirstValue(ClaimTypes.Email),
                FirstName = user.FindFirstValue(ClaimTypes.GivenName),
                LastName = user.FindFirstValue(ClaimTypes.Surname),
                PhoneNumber = user.FindFirstValue(ClaimTypes.MobilePhone),
                Role = user.FindFirstValue(ClaimTypes.Role)
            };

            return userVM;
        }

        public void AddCookies(TokenVM tokenVM, in HttpResponse httpResponse)
        {
            httpResponse.Cookies.Append("RefreshToken", tokenVM.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = tokenVM.RefreshTokenExpires
            });
            httpResponse.Cookies.Append("AccessToken", tokenVM.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = tokenVM.AccessTokenExpires
            });
        }
    }
}
