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
using AngularCircus.web.Controllers.ApiControllers;


namespace AngularCircus.web.Controllers.ApiControllers
{
    [Produces("application/json")]
    // [Route("~/act")]
    public class ActController : Controller
    {
        private readonly AngularCircusContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }
        public AngularCircusContext Context { get; set; }
        public Circus Circus { get; set; }

        public ActController(UserManager<ApplicationUser> userManager, AngularCircusContext context)
        {
            _userManager = userManager;
            _context = context;
        }



        [Route("~/act")]
        public IActionResult Act()
        {
            return View();
        }

        [HttpGet("~/api/act")]
        public IEnumerable<Act> GetActs()
        {
            var userId = _userManager.GetUserId(User);

            return _context.Acts
                .Where(q => q.Circus.Owner == userId).ToList();
        }

        // GET api/acts/5
        [HttpGet("api/act/{id}")]
        public async Task<IActionResult> GetAct(int id)
        {
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }

            var userId = _userManager.GetUserId(User);
            Act act = await _context.Acts
                .SingleOrDefaultAsync(m => m.Circus.Owner == userId && m.Circus.Id == m.Id);

            if (act == null)
            {
                return NotFound();
            }

            return Ok(act);
        }



        // POST api/acts
        [HttpPost("~/api/act")]
        public async Task<IActionResult> PostAct([FromBody]Act act)
        {
          

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            act.Owner = _userManager.GetUserId(User);
            _context.Acts.Add(act);
          
            await _context.SaveChangesAsync();
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
            var fixActs = _context.Database.ExecuteSqlCommand("update Acts set Acts.CircusId = C.Id from Acts as A inner join Circuses as C on C.Owner = A.Owner");
            return CreatedAtAction("GetAct", new { id = act.Id }, act);

        }

        // PUT api/acts/5
        [HttpPut("~/api/act/{id}")]
        public async Task<IActionResult> PutAct(int id, [FromBody] Act act)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != act.Id)
            {
                return BadRequest();
            }

            act.Owner = _userManager.GetUserId(User);
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
        [HttpDelete("~/api/act/{id}")]
        public async Task<IActionResult> DeleteAct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Act act = await _context.Acts
                .Where(q => q.Circus.Owner == userId)
                .SingleOrDefaultAsync(m => m.Circus.Id == m.Id);

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
            return _context.Acts.Any(e => e.Owner == userId && e.Id == id);
        }




    }
}
