using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularCircus.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AngularCircus.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;



namespace AngularCircus.web.Controllers.ApiControllers
{
    [Produces("application/json")]
    [Route("~/circuses/")]
    [Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
    public class CircusesController : Controller
    {
        private readonly AngularCircusContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }
        

        public CircusesController(UserManager<ApplicationUser> userManager, AngularCircusContext context)
        {
            _userManager = userManager;
            _context = context;

        }

        [Route("~/circuses/")]
        public IActionResult Circus()
        {
            return View();
        }

        [HttpGet("~/api/circuses")]
        public IEnumerable<Circus> GetCircuses()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Circuses.Where(q => q.Owner == userId).ToList();
        }

        // GET api/circuses/5
        [HttpGet("~/api/circuses/{id}")]
        public async Task<IActionResult> GetCircus(int id)
        {
            var userId = _userManager.GetUserId(User);
            Circus circus = await _context.Circuses
                .SingleOrDefaultAsync(m => m.Owner == userId && m.Id == id);

            if (circus == null)
            {
                return NotFound();
            }

            return Ok(circus);
        }



        // POST api/circuses
        [HttpPost("~/api/circuses/")]
        public async Task<IActionResult> PostCircus([FromBody]Circus circus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            circus.Owner = _userManager.GetUserId(User);
            _context.Circuses.Add(circus);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (CircusExists(circus.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetCircus", new { id = circus.Id }, circus);
        }

        // PUT api/circuses/5
        [HttpPut("~/api/circuses/{id}")]
        public async Task<IActionResult> PutCircus(int id, [FromBody] Circus circus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != circus.Id)
            {
                return BadRequest();
            }

            circus.Owner = _userManager.GetUserId(User);
            _context.Entry(circus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!CircusExists(id))
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
        [HttpDelete("~/api/circuses/{id}")]
        public async Task<IActionResult> DeleteCircus(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Circus circus = await _context.Circuses
                .Where(q => q.Owner == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if(circus == null)
            {
                return NotFound();
            }

            _context.Circuses.Remove(circus);
            await _context.SaveChangesAsync();

            return Ok(circus);
        }
        
        private bool CircusExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Circuses.Any(e => e.Owner == userId && e.Id == id);
        }
    }
}
