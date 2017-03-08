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
    public class PerformersController : Controller
    {
        private readonly AngularCircusContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }

        public PerformersController(UserManager<ApplicationUser> userManager, AngularCircusContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("~/acts/{id}/performers/")]
        //[Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
        public IActionResult Performer(int id)
        {

            var act = _context.Acts.Include(q => q.Performers).FirstOrDefault(m => m.Id == id);
            return View(act);
        }

        [Route("~/api/acts/{actId}/performers")]
        [HttpGet]
        public IEnumerable<Performer> GetPerformers()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Performers.Where(q => q.Act.Owner == userId).ToList(); //this act.Owner may be a problem
        }
        // GET api/performers/5
        [HttpGet("api/acts/{actId}/performers/{id}")]
        public async Task<IActionResult> GetPerformer(int actId)
        {
            

            var userId = _userManager.GetUserId(User);
            var act = _context.Acts.Include(q => q.Performers).FirstOrDefault(q => q.Id == actId);

            var performer = act.Performers.FirstOrDefault(q => q.Id == actId);
            //Performer Performer = await _context.Performers.SingleOrDefaultAsync(m => m.Name == userId && m.Id == id);

            if (performer == null)
            {
                return NotFound();
            }

            return Ok(performer);
        }



        // POST api/performers
        [HttpPost("~/api/acts/{actId}/performers")]
        public async Task<IActionResult> PostPerformer(int actId, [FromBody]Performer performer)
        {
            var act = _context.Acts.FirstOrDefault(q => q.Id == actId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            performer.Owner = _userManager.GetUserId(User);

            //performer.Name = _userManager.GetUserId(User);
            //_context.Performers.Add(performer);

            act.Performers.Add(performer);
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
        [HttpPut("~/api/acts/{actId}/performers{id}")]
        public async Task<IActionResult> PutPerformer(int id, [FromBody] Performer performer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != performer.Id)
            {
                return BadRequest();
            }

            performer.Owner = _userManager.GetUserId(User);
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
        [HttpDelete("~/api/performers/{id}")]
        public async Task<IActionResult> DeletePerformer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Performer Performer = await _context.Performers
                .Where(q => q.Owner == userId)
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
