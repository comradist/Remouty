using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.MVC.Configuration;
using OutOfOffice.MVC.Contracts;
using OutOfOffice.MVC.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutOfOffice.MVC.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IAuthenticateService _authService;
        private readonly LookUpTablesConfiguration lookUpTablesConfiguration;

        public IdentityController(IAuthenticateService authService, LookUpTablesConfiguration lookUpTablesConfiguration)
        {
            _authService = authService;
            this.lookUpTablesConfiguration = lookUpTablesConfiguration;
        }

        public IActionResult Login()
        {
            ViewBag.Layout = "~/Views/Identity/_IdentityLayout";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserAuthenticationVM login)
        {
            if (ModelState.IsValid)
            {
                var tokenVM = await _authService.Authenticate(login);
                _authService.AddCookies(tokenVM, Response);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Log In Attempt Failed. Please try again.");
            return View(login);
        }

        public IActionResult Register()
        {
            ViewBag.Layout = "~/Views/Identity/_IdentityLayout";
            UserRegistrationVM registration = new UserRegistrationVM()
            {
                Roles = lookUpTablesConfiguration.Roles,
            };
            return View(registration);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationVM registration)
        {
            if (ModelState.IsValid)
            {
                var returnUrl = Url.Content("~/");
                var tokenVM = await _authService.Register(registration);
                _authService.AddCookies(tokenVM, Response);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Registration Attempt Failed. Please try again.");
            return View(registration);
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            await _authService.Logout();
            Response.Cookies.Delete("AccessToken");
            Response.Cookies.Delete("RefreshToken");

            return LocalRedirect(returnUrl);
        }
    }
}
