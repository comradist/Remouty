
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<TokenDto>> Refresh([FromBody] TokenDto tokenDto)
    {
        var tokenDtoToReturn = await authenticateService.RefreshToken(tokenDto);

        return Ok(tokenDtoToReturn);
    }
}