using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularCircus.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AngularCircus.web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


namespace AngularCircus.web.Controllers.ApiControllers
{
    [Produces("application/json")]
    [Authorize]
    public class PerformerController : Controller
    {
        private readonly AngularCircusContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }
        public AngularCircusContext Context { get; set; }

        public PerformerController(UserManager<ApplicationUser> userManager, AngularCircusContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("~/performer")]
        [Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("~/api/performers")]
        [HttpGet]
        public IEnumerable<Performer> GetPerformers()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Performers.Where(q => q.Name == userId).ToList();
        }
        // GET api/performers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerformer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);
            Performer Performer = await _context.Performers
                .SingleOrDefaultAsync(m => m.Name == userId && m.Id == id);

            if (Performer == null)
            {
                return NotFound();
            }

            return Ok(Performer);
        }



        // POST api/performers
        [HttpPost]
        public async Task<IActionResult> PostPerformer([FromBody]Performer performer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            performer.Name = _userManager.GetUserId(User);
            _context.Performers.Add(performer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (PerformerExists(performer.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetPerformer", new { id = performer.Id }, performer);
        }

        // PUT api/performers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformer([FromBody]int id, [FromBody] Performer performer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != performer.Id)
            {
                return BadRequest();
            }

            performer.Name = _userManager.GetUserId(User);
            _context.Entry(performer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!PerformerExists(id))
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


        // DELETE api/performers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Performer Performer = await _context.Performers
                .Where(q => q.Name == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (Performer == null)
            {
                return NotFound();
            }

            _context.Performers.Remove(Performer);
            await _context.SaveChangesAsync();

            return Ok(Performer);

        }

        private bool PerformerExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Performers.Any(e => e.Name == userId && e.Id == id);
        }
    }

}
