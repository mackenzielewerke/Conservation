using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using TeamAuthentication.Data;

namespace TeamAuthentication.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }



        private TeamContext Context { get; set; }

        public AuthenticationController(SignInManager<ApplicationUser> signinManager,
            UserManager<ApplicationUser> userManager
            )
        {
            SignInManager = signinManager;
            UserManager = userManager;
        }

        [HttpPost]
        public async LoginRequest Login(LoginRequest model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await SignInManager.PasswordSignInAsync(user, model.Password, false, false);
                var loginRequest = new LoginRequest();
                var loginResponse = new LoginResponse();
                if (result.Succeeded)
                {
                    return Ok(loginResponse);
                }
                else if (result.IsLockedOut)
                {
                    return Ok(loginResponse);
                }
                else if (result.IsNotAllowed)
                {
                    return Ok(loginResponse);
                }
                else
                {
                    return BadRequest(loginResponse);
                }
            }
            else
            {
                return ();
            }



        }


    }

}
