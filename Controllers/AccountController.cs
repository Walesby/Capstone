using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Capstone.Models;

namespace Capstone.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> UserMgr { get; }
        private SignInManager<User> SignInMgr { get; }

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;
        }
        public async Task<IActionResult> Login()
        {
            var result = await SignInMgr.PasswordSignInAsync("TestUser", "Test123!", false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Result = "Result is: " + result.ToString();
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                //ViewBag.Message = "User already registered";
                User userExists = await UserMgr.FindByNameAsync(user.UserName);
                if(userExists == null)
                {
                    User newUser = new User();
                    newUser.UserName = user.UserName;
                    newUser.Email = user.Email;
                    newUser.FirstName = user.FirstName;
                    newUser.LastName = user.LastName;

                    IdentityResult result = await UserMgr.CreateAsync(newUser, user.PasswordHash);
                    ViewBag.Message = "User was created";
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }
    }
}