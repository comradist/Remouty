using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OutOfOffice.API.Presentation.ActionFilters;
using OutOfOffice.Contracts.Identity;
using OutOfOffice.Contracts.Infrastructure;
using OutOfOffice.Shared.DTOs.Identity;

namespace OutOfOffice.API.Presentation.Controllers;

[ApiController]
[Route("api/authenticate")]
public class AuthenticateController : ControllerBase
{
    private readonly ILoggerManager logger;
    private readonly IAuthenticateService authenticateService;

    public AuthenticateController(ILoggerManager logger, IAuthenticateService authenticateService)
    {
        this.logger = logger;
        this.authenticateService = authenticateService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistrationDto)
    {
        var result = await authenticateService.RegisterUser(userRegistrationDto);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<TokenDto>> Authenticate([FromBody] UserAuthenticationDto userAuthenticationDto)
    {
        // var tokenDto = new TokenDto("123", "456");
        // return Ok(tokenDto);
        if (!await authenticateService.ValidateUser(userAuthenticationDto))
        {
            return Unauthorized();
        }
        var tokenDto = await authenticateService.CreateToken(populateExp: true);
        return Ok(tokenDto);

    }
}