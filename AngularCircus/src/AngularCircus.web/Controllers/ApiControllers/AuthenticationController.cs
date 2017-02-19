using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using AngularCircus.web.Data;
using Microsoft.AspNetCore.Mvc;
using AngularCircus.web.Models;

namespace AngularCircus.web.Controllers.ApiControllers
{

    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        public AngularCircusContext Context { get; set; }
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }


        public AuthenticationController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            Context = new AngularCircusContext();
            SignInManager = signInManager;
            UserManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await SignInManager.PasswordSignInAsync(user, model.Password, false, true);


                if (result.Succeeded)
                {
                    return Redirect("~/");
                }
                else if (result.IsLockedOut)
                {
                    return View("LockedOut");
                }
                else if (result.IsNotAllowed)
                {
                    return View("NotAllowed");
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var user = new ApplicationUser();
            user.Email = user.UserName = model.Email;

            var result = await UserManager.CreateAsync(user, model.Password);
            await SignInManager.PasswordSignInAsync(user, model.Password, false, false);
            return View();
        }
    }
}