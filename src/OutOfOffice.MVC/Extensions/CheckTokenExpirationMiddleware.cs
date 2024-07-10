using System.IdentityModel.Tokens.Jwt;
using OutOfOffice.MVC.Services.Base;

namespace OutOfOffice.MVC.Extensions;
public class CheckTokenExpirationMiddleware
{
    private readonly RequestDelegate _next;
    private TokenDto tokenDto = new();
    private readonly IClient client;
    private readonly string jwtCookieName;
    private readonly string refreshTokenCookieName;

    public CheckTokenExpirationMiddleware(RequestDelegate next, IClient client, string jwtCookieName = "AccessToken", string refreshTokenCookieName = "RefreshToken")
    {
        _next = next;
        this.client = client;
        this.jwtCookieName = jwtCookieName;
        this.refreshTokenCookieName = refreshTokenCookieName;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            if (context.Request.Cookies.TryGetValue(jwtCookieName, out var jwtToken) &&
            context.Request.Cookies.TryGetValue(refreshTokenCookieName, out var refreshToken))
            {
                tokenDto.AccessToken = jwtToken;
                tokenDto.RefreshToken = refreshToken;

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(jwtToken);

                //var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                // var expiryTime = unixEpoch.AddSeconds(Convert.ToDouble(claims.FirstOrDefault(x => x.Type == "exp")?.Value));
                // var expiryTime2 = unixEpoch.AddSeconds(Convert.ToDouble(tokenAccessContent.Payload.ValidTo));
                // var expiryTime3 = unixEpoch.AddSeconds(Convert.ToDouble(tokenAccessContent.Payload.ValidFrom));
                //var expiryTime4 = unixEpoch.AddSeconds(Convert.ToDouble(token.Payload.Exp));

                var expirationAccessToken = token.ValidTo;

                var now = DateTime.UtcNow.AddHours(3);
                var timeAccessTokenUntilExpiration = expirationAccessToken - now;

                if (timeAccessTokenUntilExpiration < TimeSpan.FromMinutes(10))
                {

                    var tokenAPIResponse = await client.TokenAsync(tokenDto);
                    if (tokenAPIResponse.StatusCode == StatusCodes.Status200OK)
                    {
                        context.Response.Cookies.Append(jwtCookieName, tokenAPIResponse.Result.AccessToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = token.ValidTo,
                        });
                        context.Response.Cookies.Append(refreshTokenCookieName, tokenAPIResponse.Result.RefreshToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTime.UtcNow.AddDays(7)
                        });
                    }
                    else
                    {
                        context.Response.Redirect("/Identity/Login");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            context.Response.Redirect("/Identity/Login");
            context.Response.Cookies.Delete("AccessToken");
            context.Response.Cookies.Delete("RefreshToken");
        }
        await _next.Invoke(context);
    }    
}


