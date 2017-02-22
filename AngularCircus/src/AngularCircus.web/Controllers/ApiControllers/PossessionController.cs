using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using AngularCircus.web.Data;
using AngularCircus.web.Models;

namespace AngularCircus.web.Controllers.ApiControllers
{
    [Produces("application/json")]
    [Route("api/possessions")]
    [Authorize]
    public class PossessionsController : Controller
    {
        private readonly AngularCircusContext _context;
        private UserManager<ApplicationUser> _userManager { get; set; }

        public PossessionsController(UserManager<ApplicationUser> userManager, AngularCircusContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("~/possessions")]
        public IActionResult Index()
        {
            return View();
        }

        // GET: api/Possessions
        [HttpGet]
        public IEnumerable<Possession> GetPossessions()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Possessions.Where(q => q.Owner == userId).ToList();
        }

        // GET: api/Possessions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPossession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);
            Possession possession = await _context.Possessions
                .SingleOrDefaultAsync(m => m.Owner == userId && m.Id == id);

            if (possession == null)
            {
                return NotFound();
            }

            return Ok(possession);
        }

        // PUT: api/Possessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPossession([FromRoute] int id, [FromBody] Possession possession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != possession.Id)
            {
                return BadRequest();
            }

            possession.Owner = _userManager.GetUserId(User);
            _context.Entry(possession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PossessionExists(id))
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

        // POST: api/Possessions
        [HttpPost]
        public async Task<IActionResult> PostPossession([FromBody] Possession possession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            possession.Owner = _userManager.GetUserId(User);
            _context.Possessions.Add(possession);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PossessionExists(possession.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPossession", new { id = possession.Id }, possession);
        }

        // DELETE: api/Possessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePossession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Possession possession = await _context.Possessions
                .Where(q => q.Owner == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (possession == null)
            {
                return NotFound();
            }

            _context.Possessions.Remove(possession);
            await _context.SaveChangesAsync();

            return Ok(possession);
        }

        private bool PossessionExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Possessions.Any(e => e.Owner == userId && e.Id == id);
        }
    }
}