using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Conservation.web.Data;
using Conservation.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Conservation.web.Controllers
{
    [Produces("application/json")]
    [Route("api/Conservations")]
    public class ConservationsController : Controller
    {
        private readonly ConservationContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }


        public ConservationsController(UserManager<ApplicationUser> userManager, ConservationContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("~/conservations")]
        [Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
        public IActionResult Conservation()
        {
            return View();
        }

        [HttpGet]
        [Route("~/api/conservations")]
        public IEnumerable<Conservations> GetConservations()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Conservations.Where(q => q.Owner == userId).ToList();
        }

        [HttpGet]
        [Route("~/api/conservations/{id}")]
        public async Task<IActionResult> GetConservation(int id)
        {
            var userId = _userManager.GetUserId(User);
            Conservations conservation = await _context.Conservations
                .SingleOrDefaultAsync(m => m.Owner == userId && m.Id == id);

            if (conservation == null)
            {
                return NotFound();
            }

            return Ok(conservation);
        }


        [HttpPost]
        [Route("~/api/conservations")]
        public async Task<IActionResult> PostConservation([FromBody]Conservations conservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            conservation.Owner = _userManager.GetUserId(User);
            _context.Conservations.Add(conservation);
            await _context.SaveChangesAsync();

            return View();
        }

        // PUT api/circuses/5
        [HttpPut("~/api/conservations/{id}")]
        public async Task<IActionResult> PutConsesrvation(int id, [FromBody] Conservations conservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conservation.Id)
            {
                return BadRequest(Response);
            }

            conservation.Owner = _userManager.GetUserId(User);
            _context.Entry(conservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE api/circuses/5
        [HttpDelete("~/api/conservations/{id}")]
        public async Task<IActionResult> DeleteConservation(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Conservations conservation = await _context.Conservations
                .Where(q => q.Owner == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (conservation == null)
            {
                return NotFound();
            }

            _context.Conservations.Remove(conservation);
            await _context.SaveChangesAsync();

            return Ok(conservation);
        }

        private bool ConservationExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Conservations.Any(e => e.Owner == userId && e.Id == id);
        }

    }
}