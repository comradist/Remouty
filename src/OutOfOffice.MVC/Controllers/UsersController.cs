using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.MVC.Contracts;
using OutOfOffice.MVC.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.LeaveManagement.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAuthenticateService _authService;

        public UsersController(IAuthenticateService authService)
        {
            this._authService = authService;
        }

        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserAuthenticationVM login)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Authenticate(login.UserNameOrEmail, login.Password);
                Response.Cookies.Append("RefreshToken", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.RefreshTokenExpires
                });
                Response.Cookies.Append("AccessToken", result.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.AccessTokenExpires
                });
                
                
                return RedirectToAction("Index", "Home");
                //return Json(new { success = true, accessToken = result.AccessToken, refreshToken = result.RefreshToken });
            }
            ModelState.AddModelError("", "Log In Attempt Failed. Please try again.");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationVM registration)
        {
            if (ModelState.IsValid)
            {
                var returnUrl = Url.Content("~/");
                await _authService.Register(registration);
                // if (isCreated)
                //     return LocalRedirect(returnUrl);
            }
            
            ModelState.AddModelError("", "Registration Attempt Failed. Please try again.");
            return View(registration);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            await _authService.Logout();
            Response.Cookies.Delete("AccessToken, RefreshToken");
            return LocalRedirect(returnUrl);
        }
    }
}
