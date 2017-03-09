using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularZoo.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AngularZoo.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;



namespace AngularConservation.web.Controllers.ApiControllers
{
    [Produces("application/json")]
    //[Route("~/circus")]
    [Authorize]
    public class ConservationsController : Controller
    {
        private readonly AngularConservationContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }
        

        public ConservationsController(UserManager<ApplicationUser> userManager, AngularConservationContext context)
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

        [HttpGet("~/api/conservations")]
        public IEnumerable<Conservation> GetConservations()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Conservations.Where(q => q.Owner == userId).ToList();
        }

        // GET api/circuses/5
        [HttpGet("~/api/conservations/{id}")]
        public async Task<IActionResult> GetConservation(int id)
        {

            var userId = _userManager.GetUserId(User);
            Conservation conservation = await _context.Conservations
                .SingleOrDefaultAsync(m => m.Owner == userId && m.Id == id);

            if (conservation == null)
            {
                return NotFound();
            }

            return Ok(conservation);
        }



        // POST api/circuses
        [HttpPost("~/api/conservations")]
        public async Task<IActionResult> PostConservation([FromBody]Conservation conservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            conservation.Owner = _userManager.GetUserId(User);
            _context.Conservations.Add(conservation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (ConservationExists(conservation.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetConservation", new { id = conservation.Id }, conservation);
        }

        // PUT api/circuses/5
        [HttpPut("~/api/conservations/{id}")]
        public async Task<IActionResult> PutConsesrvation(int id, [FromBody] Conservation conservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conservation.Id)
            {
                return BadRequest();
            }

            conservation.Owner = _userManager.GetUserId(User);
            _context.Entry(conservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ConservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }


        // DELETE api/circuses/5
        [HttpDelete("~/api/conservations/{id}")]
        public async Task<IActionResult> DeleteConservation(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Conservation conservation = await _context.Conservations
                .Where(q => q.Owner == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if(conservation == null)
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
