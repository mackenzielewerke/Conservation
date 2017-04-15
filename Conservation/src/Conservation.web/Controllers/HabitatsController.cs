using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Conservation.web.Data;
using Microsoft.AspNetCore.Identity;
using Conservation.web.Models;
using Microsoft.EntityFrameworkCore;

namespace Conservation.web.Controllers
{
    [Produces("application/json")]
    [Route("api/Habitats")]
    public class HabitatsController : Controller
    {
        private readonly ConservationContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }


        public HabitatsController(UserManager<ApplicationUser> userManager, ConservationContext context)
        {
            _userManager = userManager;
            _context = context;
        }



        [Route("~/conservations/{id}/habitats")]
        public IActionResult Habitat(int id)
        {
            var conservation = _context.Conservations.Include(q => q.Habitats).FirstOrDefault(m => m.Id == id);
            return View(conservation);
        }

        [HttpGet("~/api/conservations/{conservationId}/habitats")]
        public IEnumerable<Habitat> GetHabitats()
        {
            var userId = _userManager.GetUserId(User);


            return _context.Habitats.Where(q => q.Conservation.Owner == userId).ToList();
        }

        // GET api/acts/5
        [HttpGet("~/api/conservations/{conservationId}/habitats/{id}")] //"api/zoos/{zooId}/exhibits/{id}
        public async Task<IActionResult> GetHabitat(int conservationId) //, int id) //he only had one
        {


            var userId = _userManager.GetUserId(User);

            var conservation = _context.Conservations.Include(q => q.Habitats).FirstOrDefault(q => q.Id == conservationId);
            var habitat = conservation.Habitats.FirstOrDefault(q => q.Id == conservationId); //== id); //not sure if need circusId or id. He had one and hten the other soemhwere

            if (habitat == null)
            {
                return NotFound();
            }

            return Ok(habitat);
        }



        // POST api/acts
        [HttpPost("~/api/conservations/{conservationId}/habitats")]
        public async Task<IActionResult> PostGroup(int conservationId, [FromBody]Habitat habitat)
        {
            var conservation = _context.Conservations.FirstOrDefault(q => q.Id == conservationId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            habitat.Owner = _userManager.GetUserId(User);

            conservation.Habitats.Add(habitat);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (HabitatExists(habitat.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //var fixActs = _context.Database.ExecuteSqlCommand("update Acts set Acts.CircusId = C.Id from Acts as A inner join Circuses as C on C.Owner = A.Owner");
            return CreatedAtAction("GetGroup", new { id = habitat.Id }, habitat);

        }

        // PUT api/acts/5
        [HttpPut("~/api/conservations/{conservationId}/habitats/{id}")]
        public async Task<IActionResult> PutHabitat(int id, [FromBody] Habitat habitat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != habitat.Id)
            {
                return BadRequest();
            }

            habitat.Owner = _userManager.GetUserId(User);
            _context.Entry(habitat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!HabitatExists(id))
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
        [HttpDelete("~/api/habitats/{id}")]
        public async Task<IActionResult> DeleteHabitat(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Habitat habitat = await _context.Habitats
                .Where(q => q.Owner == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (habitat == null)
            {
                return NotFound();
            }

            _context.Habitats.Remove(habitat);
            await _context.SaveChangesAsync();

            return Ok(habitat);

        }

        private bool HabitatExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Habitats.Any(e => e.Owner == userId && e.Id == id);
        }
    }
}