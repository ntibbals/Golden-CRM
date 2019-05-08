using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Golden_CRM.Models;
using Golden_CRM.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Golden_CRM.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName,
                    UserName = rvm.Email,
                    Email = rvm.Email
 
                };

                var result = await _userManager.CreateAsync(user, rvm.Password);

                if (result.Succeeded)
                {
                    Claim name = new Claim("Name", user.FirstName);
                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email);
                    Claim userEmailClaim = new Claim("Email", user.Email);

                    List<Claim> claims = new List<Claim> { name, emailClaim, userEmailClaim };

                    await _userManager.AddClaimsAsync(user, claims);

                    if  (user.Email == "ntibbals@outlook.com")
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    //if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    //{

                    //    return LocalRedirect("~/Admin/Dashboard");
                    //}

                    return RedirectToAction("Index", "Home");

                }
            }

            return View(rvm);
        }

        [HttpGet]
        public IActionResult Login() => View();

        /// <summary>
        /// Method to log user into site
        /// </summary>
        /// <param name="loginVM">Login View Model</param>
        /// <returns>View of Home Page</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    var ourUser = await _userManager.FindByEmailAsync(loginVM.Email);


                    if (await _userManager.IsInRoleAsync(ourUser, ApplicationRoles.Admin))
                    {
                        return RedirectToAction("Index", "Home");
                        //return LocalRedirect("~/Admin/Dashboard");

                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            return View(loginVM);
        }

        /// <summary>
        /// This method logs the user out
        /// </summary>
        /// <returns>Home Page View</returns>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}