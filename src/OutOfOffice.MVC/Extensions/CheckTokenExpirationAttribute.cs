using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.Filters;
using OutOfOffice.MVC.Services.Base;

namespace OutOfOffice.MVC.Extensions;

public class CheckTokenExpirationAttribute : ActionFilterAttribute
{
    private TokenDto tokenDto = new();
    private readonly IClient client;
    private readonly string jwtCookieName;
    private readonly string refreshTokenCookieName;

    public CheckTokenExpirationAttribute(IClient client, string jwtCookieName = "AccessToken", string refreshTokenCookieName = "RefreshToken")
    {
        this.client = client;
        this.jwtCookieName = jwtCookieName;
        this.refreshTokenCookieName = refreshTokenCookieName;
    }

    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;
        if(httpContext.Request.Cookies.TryGetValue(jwtCookieName, out var jwtToken) && 
            httpContext.Request.Cookies.TryGetValue(refreshTokenCookieName, out var refreshToken))
        {
            tokenDto.AccessToken = jwtToken;
            tokenDto.RefreshToken = refreshToken;

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);

            var expirationAccessToken = token.ValidTo;
            var exceptionRefreshToken = token.ValidTo;

            var now = DateTime.UtcNow;
            var timeAccessTokenUntilExpiration = expirationAccessToken - now;
            var timeRefreshTokenUntilExpiration = exceptionRefreshToken - now;

            if (timeRefreshTokenUntilExpiration < TimeSpan.FromMinutes(60))
            {
                httpContext.Response.Redirect("/Identity/Login");
            }

            if(timeAccessTokenUntilExpiration < TimeSpan.FromMinutes(5))
            {
                var tokenAPIResponse = await client.TokenAsync(tokenDto);
                if(tokenAPIResponse.StatusCode == StatusCodes.Status201Created)
                {
                    httpContext.Response.Cookies.Append(jwtCookieName, tokenAPIResponse.Result.AccessToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddMinutes(60)
                    });
                    httpContext.Response.Cookies.Append(refreshTokenCookieName, tokenAPIResponse.Result.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddDays(7)
                    });
                }
                else
                {
                    httpContext.Response.Redirect("/Identity/Login");
                }
            }
        }
        else
        {
            httpContext.Response.Redirect("/Identity/Login");
        }


    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // base.OnActionExecuted(context);
    }
}