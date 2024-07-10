using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.MVC.Contracts;
using OutOfOffice.MVC.Models;
using OutOfOffice.MVC.Extensions;
using OutOfOffice.MVC.Models.Identity;

namespace OutOfOffice.MVC.Controllers;

[Authorize]
public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly IAuthenticateService authenticationService;
    private readonly IHttpContextAccessor httpContextAccessor;

    public HomeController(ILogger<HomeController> logger, IAuthenticateService authenticationService, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        this.authenticationService = authenticationService;
        this.httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        var k = httpContextAccessor.HttpContext.User.Claims;
        // httpContextAccessor.HttpContext.User.Claims
        // UserVM newUser = new UserVM
        // {
        //     Role = httpContextAccessor.HttpContext.User.Claims
        // }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
