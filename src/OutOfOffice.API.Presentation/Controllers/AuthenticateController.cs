using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto userAuthenticationDto)
    {
        if (!await authenticateService.ValidateUser(userAuthenticationDto))
        {
            return Unauthorized();
        }
        var tokenDto = await authenticateService.CreateToken(populateExp: true);
        return Ok(tokenDto);

    }
}