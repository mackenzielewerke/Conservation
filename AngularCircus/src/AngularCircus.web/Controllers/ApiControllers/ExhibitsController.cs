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
    public class ExhibitsController : Controller
    {
        private readonly AngularZooContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }
        

        public ExhibitsController(UserManager<ApplicationUser> userManager, AngularZooContext context)
        {
            _userManager = userManager;
            _context = context;
        }



        [Route("~/zoos/{id}/exhibits")]
        public IActionResult Exhibit(int id)
        {
            var zoo = _context.Zoos.Include(q => q.Exhibits).FirstOrDefault(m => m.Id == id);
            return View(zoo);
        }

        [HttpGet("~/api/zoos/{zooId}/exhibits")]
        public IEnumerable<Exhibit> GetExhibits()
        {
            var userId = _userManager.GetUserId(User);
            

            return _context.Exhibits.Where(q => q.Zoo.Owner == userId).ToList();
        }

        // GET api/acts/5
        [HttpGet("api/zoos/{zooId}/exhibits/{id}")]
        public async Task<IActionResult> GetExhibit(int zooId) //, int id) //he only had one
        {
            

            var userId = _userManager.GetUserId(User);

            var zoo = _context.Zoos.Include(q => q.Exhibits).FirstOrDefault(q => q.Id == zooId);
            var exhibit = zoo.Exhibits.FirstOrDefault(q => q.Id == zooId); //== id); //not sure if need circusId or id. He had one and hten the other soemhwere

            if (exhibit == null)
            {
                return NotFound();
            }

            return Ok(exhibit);
        }



        // POST api/acts
        [HttpPost("~/api/zoos/{zooId}/exhibits")]
        public async Task<IActionResult> PostExhibit(int zooId, [FromBody]Exhibit exhibit)
        {
            var zoo = _context.Zoos.FirstOrDefault(q => q.Id == zooId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            exhibit.Owner =  _userManager.GetUserId(User);

            zoo.Exhibits.Add(exhibit);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (ExhibitExists(exhibit.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //var fixActs = _context.Database.ExecuteSqlCommand("update Acts set Acts.CircusId = C.Id from Acts as A inner join Circuses as C on C.Owner = A.Owner");
            return CreatedAtAction("GetExhibit", new { id = exhibit.Id }, exhibit);

        }

        // PUT api/acts/5
        [HttpPut("~/api/zoos/{zooId}/exhibits/{id}")]
        public async Task<IActionResult> PutExhibit(int id, [FromBody] Exhibit exhibit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exhibit.Id)
            {
                return BadRequest();
            }

            exhibit.Owner = _userManager.GetUserId(User);
            _context.Entry(exhibit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ExhibitExists(id))
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
        [HttpDelete("~/api/exhibits/{id}")]
        public async Task<IActionResult> DeleteExhibit(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Exhibit exhibit = await _context.Exhibits
                .Where(q => q.Owner == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (exhibit == null)
            {
                return NotFound();
            }

            _context.Exhibits.Remove(exhibit);
            await _context.SaveChangesAsync();

            return Ok(exhibit);

        }

        private bool ExhibitExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Exhibits.Any(e => e.Owner == userId && e.Id == id);
        }




    }
}
