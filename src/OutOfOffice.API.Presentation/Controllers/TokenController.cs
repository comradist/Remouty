
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.API.Presentation.ActionFilters;
using OutOfOffice.Contracts.Identity;
using OutOfOffice.Shared.DTOs.Identity;

namespace OutOfOffice.API.Presentation.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IAuthenticateService authenticateService;

    public TokenController(IAuthenticateService authenticateService)
    {
        this.authenticateService = authenticateService;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var tokenDtoToReturn = await authenticateService.RefreshToken(tokenDto);

        return Ok(tokenDtoToReturn);
    }
}