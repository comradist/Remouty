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

        public async Task<TokenVM> Authenticate(string userNameOrEmail, string password)
        {

            UserAuthenticationDto userAuthentication = new() { UserNameOrEmail = userNameOrEmail, Password = password };
            var authenticationResponse = await _client.LoginAsync(userAuthentication);

            if (authenticationResponse.Result.AccessToken == string.Empty && authenticationResponse.Result.RefreshToken == string.Empty)
            {
                throw new Exception("An error occurred while trying to authenticate the user.");    
            }
            //Get Claims from token and Build auth user object
            var tokenAccessContent = _tokenHandler.ReadJwtToken(authenticationResponse.Result.AccessToken);
            var claims = ParseClaims(tokenAccessContent);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

            var tokenVm = _mapper.Map<TokenVM>(authenticationResponse);

            // var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            // var expiryTime = unixEpoch.AddSeconds(Convert.ToDouble(claims.FirstOrDefault(x => x.Type == "exp")?.Value));
            // var expiryTime2 = unixEpoch.AddSeconds(Convert.ToDouble(tokenAccessContent.Payload.ValidTo));
            // var expiryTime3 = unixEpoch.AddSeconds(Convert.ToDouble(tokenAccessContent.Payload.ValidFrom));
            // var expiryTime4 = unixEpoch.AddSeconds(Convert.ToDouble(tokenAccessContent.Payload.Expiration));
            //tokenVm.AccessTokenExpires = expiryTime2;

            return tokenVm;
        }

        public async Task Register(UserRegistrationVM registration)
        {

            UserRegistrationDto userRegistration = _mapper.Map<UserRegistrationDto>(registration);
            await _client.RegisterAsync(userRegistration);

            // if (!string.IsNullOrEmpty(response.UserId))
            // {
            //     await Authenticate(registration.Email, registration.Password);
            //     return true;
            // }
            // return false;
        }

        public async Task Logout()
        {
            //_localStorage.ClearStorage(new List<string> { "token", "tokenRefresh" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {

            var claims = tokenContent.Claims.ToList();
            //claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
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

    }
}
