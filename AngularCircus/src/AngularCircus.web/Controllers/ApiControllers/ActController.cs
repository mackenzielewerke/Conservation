using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularCircus.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AngularCircus.web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace AngularCircus.web.Controllers.ApiControllers
{
    [Produces("application/json")]
    [Route("api/act")]
    [Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
    public class ActController : Controller
    {
        private readonly AngularCircusContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }
        public AngularCircusContext Context { get; set; }

        public ActController(UserManager<ApplicationUser> userManager, AngularCircusContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("~/act")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IEnumerable<Act> GetActs()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Acts.Where(q => q.Name == userId).ToList();
        }
        // GET api/acts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);
            Act act = await _context.Acts
                .SingleOrDefaultAsync(m => m.Name == userId && m.Id == id);

            if (act == null)
            {
                return NotFound();
            }

            return Ok(act);
        }



        // POST api/acts
        [HttpPost]
        public async Task<IActionResult> PostAct ([FromBody]Act act)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            act.Name = _userManager.GetUserId(User);
            _context.Acts.Add(act);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (ActExists(act.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetAct", new { id = act.Id }, act);
        }

        // PUT api/acts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAct([FromBody]int id, [FromBody] Act act)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != act.Id)
            {
                return BadRequest();
            }

            act.Name = _userManager.GetUserId(User);
            _context.Entry(act).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ActExists(id))
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Act act = await _context.Acts
                .Where(q => q.Name == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (act == null)
            {
                return NotFound();
            }

            _context.Acts.Remove(act);
            await _context.SaveChangesAsync();

            return Ok(act);

        }

        private bool ActExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Acts.Any(e => e.Name == userId && e.Id == id);
        }
    }
}
