using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OutOfOffice.MVC.Contracts;

namespace OutOfOffice.MVC.Extensions;

public class UserInfoFilter : IActionFilter
{
    private readonly IAuthenticateService authenticateService;

    public UserInfoFilter(IAuthenticateService authenticateService)
    {
        this.authenticateService = authenticateService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.Controller as Controller;
        if (controller != null)
        {
            var user = authenticateService.GetCurrentUser(); // Method to get the current user
            controller.ViewData["CurrentUser"] = user;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Do nothing
    }
}
