﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Conservation.web.Models;
using Conservation.web.Services;

namespace Conservation.web.Controllers
{
    public class AccountsController : Controller
    {
        public UserManager<ApplicationUser> _userManager;
        public SignInManager<ApplicationUser> _signInManager;


        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("~/account/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("~/account/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);


                if (result.Succeeded)
                {
                    return Ok(result.Succeeded);
                }
                else
                {
                    return BadRequest("Something went wrong. Try again");
                }       
            }
            return View(model);
        }

        [HttpGet]
        [Route("~/account/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("~/account/register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var user = new ApplicationUser();
            user.Email = user.UserName = model.Email;
            user.Signature = Guid.NewGuid();

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                return Ok(result.Succeeded);
            }
            else
            {
                return BadRequest("Something went wrong. Try Again");
            }
        }


        [HttpPost]
        [Route("~/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("~/");
        }

 
    }
}
