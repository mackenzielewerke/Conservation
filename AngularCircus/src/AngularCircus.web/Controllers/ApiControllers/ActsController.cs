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
    public class ActsController : Controller
    {
        private readonly AngularCircusContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }
        

        public ActsController(UserManager<ApplicationUser> userManager, AngularCircusContext context)
        {
            _userManager = userManager;
            _context = context;
        }



        [Route("~/circuses/{id}/acts")]
        public IActionResult Act(int id)
        {
            var circus = _context.Circuses.Include(q => q.Acts).FirstOrDefault(m => m.Id == id);
            return View(circus);
        }

        [HttpGet("~/api/circuses/{circusId}/acts")]
        public IEnumerable<Act> GetActs()
        {
            var userId = _userManager.GetUserId(User);
            

            return _context.Acts
                .Where(q => q.Circus.Owner == userId).ToList();
        }

        // GET api/acts/5
        [HttpGet("api/circuses/{circusId}/acts/{id}")]
        public async Task<IActionResult> GetAct(int circusId, int id)
        {
            

            var userId = _userManager.GetUserId(User);

            var circus = _context.Circuses.Include(q => q.Acts).FirstOrDefault(q => q.Id == circusId);
            var act = circus.Acts.FirstOrDefault(q => q.Id == id);

            if (act == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return Ok(act);
        }



        // POST api/acts
        [HttpPost("~/api/circuses/{circusId}/acts")]
        public async Task<IActionResult> PostAct(int circusId, [FromBody]Act act)
        {
            var circus = _context.Circuses.FirstOrDefault(q => q.Id == circusId);
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            act.Owner =  _userManager.GetUserId(User);

            circus.Acts.Add(act);

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
            //var fixActs = _context.Database.ExecuteSqlCommand("update Acts set Acts.CircusId = C.Id from Acts as A inner join Circuses as C on C.Owner = A.Owner");
            return CreatedAtAction("GetAct", new { circusId = act.Id }, act);

        }

        // PUT api/acts/5
        [HttpPut("~/api/circuses/{circusId}/acts/{id}")]
        public async Task<IActionResult> PutAct(int circusId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (circusId != act.Id)
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
                if (!ActExists(circusId))
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
        [HttpDelete("~/api/circuses/{circusId}/acts/{id}")]
        public async Task<IActionResult> DeleteAct(int circusId, [FromBody] Act act)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            act = await _context.Acts
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

        private bool ActExists(int circusId)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Acts.Any(e => e.Owner == userId && e.Id == circusId);
        }




    }
}
