using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AngularCircus.web.Models;

using Microsoft.AspNetCore.Identity;

using AngularCircus.web.Data;

namespace Authentication.Web.Controllers
{
    [Produces("application/json")]
    [Route("authentication")]
    public class AuthenticationController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        public SignInManager<ApplicationUser> SignInManager { get; set; }

        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        [Route("~/authentication/logins")]
        public IActionResult Logins()
        {
            return View();
        }

        [Route("~/authentication/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("~/authentication/register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest model)
        {
            var user = new ApplicationUser();
            user.Email = user.UserName = model.Email;
            user.Signature = Guid.NewGuid();

            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return View(model);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("~/authentication/logins")]
        public async Task<IActionResult> Login([FromBody]LoginRequest model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                await SignInManager.SignInAsync(user, false);
                return Ok();
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