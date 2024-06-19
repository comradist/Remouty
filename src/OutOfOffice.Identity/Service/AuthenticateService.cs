using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyMind.Application.Contracts;
using MyMind.Application.DTOs.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;  
using Microsoft.Extensions.Options;
using MyMind.Domain.ConfigurationModels;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using MyMind.Application.Contracts.Identity;
using MyMind.Application.Models.Identity;

namespace MyMind.Identity.Service;

public class AuthenticateService : IAuthenticateService
{
    private User? userApp;

    private readonly UserManager<User> userManager;
    
    private readonly RoleManager<IdentityRole> identityRole;

    private readonly JwtConfiguration jwtSettings;

    private readonly IMapper mapper;

    private readonly ILoggerManager logger;

    public AuthenticateService(UserManager<User> userManager, RoleManager<IdentityRole> identityRole, IMapper mapper, ILoggerManager loggerManager, IOptionsMonitor<JwtConfiguration> options)
    {
        this.userManager = userManager;
        this.identityRole = identityRole;
        this.mapper = mapper;
        this.logger = loggerManager;
        jwtSettings = options.CurrentValue;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="userAuthenticationDto">The user authentication data.</param>
    /// <returns>The result of the user registration.</returns>
    public async Task<IdentityResult> RegisterUser(UserRegistrationDto userAuthenticationDto)
    {
        var user = mapper.Map<User>(userAuthenticationDto);
        var userExist = await userManager.FindByNameAsync(user.UserName!);
        var emailExist = await userManager.FindByEmailAsync(user.Email!);
        if (userExist != null)
        {
            //TODO add ex
            throw new Exception("User already exist");
        }

        if (emailExist != null)
        {
            //TODO add ex
            throw new Exception("Email already exist");
        }
        var emailToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
        
        var userCreateResult = await userManager.CreateAsync(user, userAuthenticationDto.Password);
        if (userCreateResult.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
            return userCreateResult;
        }
        else
        {
            //TODO add ex
            throw new Exception();
        }
    }

    /// <summary>
    /// Validates a user's credentials.
    /// </summary>
    /// <param name="userAuthenticationDto">The user authentication data.</param>
    /// <returns>True if the user's credentials are valid, otherwise false.</returns>
    public async Task<bool> ValidateUser(UserAuthenticationDto userAuthenticationDto)
    {
        var user = userAuthenticationDto.UserNameOrEmail.Contains('@') ? 
            await userManager.FindByEmailAsync(userAuthenticationDto.UserNameOrEmail) : 
            await userManager.FindByNameAsync(userAuthenticationDto.UserNameOrEmail);

        var result = user != null && await userManager.CheckPasswordAsync(user, userAuthenticationDto.Password);
        if(!result)
        {
            logger.LogError($"User {userAuthenticationDto.UserNameOrEmail} failed to authenticate");
        }
        this.userApp = user;
        return result;
    }

    /// <summary>
    /// Creates a token for authentication.
    /// </summary>
    /// <param name="populateExp">A boolean value indicating whether to populate the refresh token expiry time.</param>
    /// <returns>A <see cref="TokenDto"/> object containing the access token and refresh token.</returns>
    public async Task<TokenDto> CreateToken(bool populateExp)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaimsAsync();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        var refreshToken = GenerateRefreshToken();

        userApp.RefreshToken = refreshToken;

        if (populateExp)
        {
            userApp.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        }

        await userManager.UpdateAsync(userApp);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new TokenDto(accessToken, refreshToken);
    }

    /// <summary>
    /// Refreshes the access token using the provided token DTO.
    /// </summary>
    /// <param name="tokenDto">The token DTO containing the access token and refresh token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the refreshed access token.</returns>
    public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
    {
        var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

        var user = await userManager.FindByNameAsync(principal.Identity.Name);

        if (user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new SecurityTokenException("Invalid token");
        }

        this.userApp = user;
        return await CreateToken(true);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };

        //When token is invalid, it will throw an exception, because of the ValidateLifetime = true(fixed to false)
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return principal;
    }

    public async Task<List<Claim>> GetClaimsAsync()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userApp.UserName),
        };

        var roles = await userManager.GetRolesAsync(userApp);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtSettings.TokenExpiration.Minutes),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

}