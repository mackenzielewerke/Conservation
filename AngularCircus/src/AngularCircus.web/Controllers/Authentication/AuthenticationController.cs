using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using AngularCircus.web.Data;
using Microsoft.AspNetCore.Mvc;
using AngularCircus.web.Models;
using Microsoft.AspNetCore.Authorization;

namespace AngularCircus.web.Controllers.Authentication
{
    [Produces("application/json")]
    [Route("~/authentication")]
    [Authorize]
    public class AuthenticationController : Controller
    {
        public SignInManager<ApplicationUser> SignInManager { get; set; }

        public UserManager<ApplicationUser> UserManager { get; set; }
        public AuthenticationController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }

        [AllowAnonymous]
        [Route("~/authentication/login")]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("~/authentication/login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await SignInManager.PasswordSignInAsync(user, model.Password, false, true);


                if (result.Succeeded)
                {
                    Redirect("~/circuses");
                    return Ok(result.Succeeded);

                }
                else if (result.IsLockedOut)
                {
                    return BadRequest(result);
                }
                else if (result.IsNotAllowed)
                {
                    return BadRequest(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [Route("~/authentication/register")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("~/authentication/register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest model)
        {
            var user = new ApplicationUser();
            user.Email = user.UserName = model.Email;
            user.Signature = Guid.NewGuid();

            var result = await UserManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                await SignInManager.PasswordSignInAsync(user, model.Password, false, false);
                return Ok(result.Succeeded);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("~/authentication/logout")]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}