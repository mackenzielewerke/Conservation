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
                .Include(b => b.Circus)
                .Where(q => q.Circus.Owner == userId)
                .ToList();
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
                .Include(b => b.Circus)
                .SingleOrDefaultAsync(m => m.Circus.Owner == userId && m.CircusId == m.Id);

            if (act == null)
            {
                return NotFound();
            }

            return Ok(act);
        }



        // POST api/acts
        [HttpPost("~/api/act")]
        public async Task<IActionResult> PostAct([FromBody] ActRequest model)
        {
            _context.Acts
                .Include(b => b.Circus)
                .FirstOrDefault(a => a.CircusId == a.Id && a.Name == model.Name);


            //if (act != null)
            //{
            //    return BadRequest("Act with name " + name + " already exists. ");
            //}

            var act = new Act()
            {
                Name = model.Name
            };
            _context.Acts.Add(act);
            act.Owner = _userManager.GetUserId(User);           

            await _context.SaveChangesAsync();

            return Ok(model);

        }

        // PUT api/acts/5
        [HttpPut("~/api/act/{id}")]
        public async Task<IActionResult> PutAct(int id, [FromBody] Act act)
        {
            act = await _context.Acts.FirstOrDefaultAsync(m => m.CircusId == m.Id);
            if (act != null)
            {
                return BadRequest();
            }

            act.Owner = _userManager.GetUserId(User);
            _context.Entry(act).State = EntityState.Modified;

            return Ok();
        }


        // DELETE api/circuses/5
        [HttpDelete("~/api/act/{id}")]
        public async Task<IActionResult> DeleteAct(int id)
        {

            var userId = _userManager.GetUserId(User);

            Act act = await _context.Acts
                .Where(q => q.Circus.Owner == userId)
                .SingleOrDefaultAsync(m => m.CircusId == m.Id);

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
            return _context.Acts.Any(e => e.Owner == userId && e.CircusId == e.Id);
        }
    }
}
