using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularZoo.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AngularZoo.web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AngularZoo.web.Controllers.ApiControllers;


namespace AngularZoo.web.Controllers.ApiControllers
{
    [Produces("application/json")]
    // [Route("~/act")]
    public class GroupsController : Controller
    {
        private readonly AngularConservationContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }
        

        public GroupsController(UserManager<ApplicationUser> userManager, AngularConservationContext context)
        {
            _userManager = userManager;
            _context = context;
        }



        [Route("~/conservations/{id}/groupss")]
        public IActionResult Group(int id)
        {
            var conservation = _context.Conservations.Include(q => q.Groups).FirstOrDefault(m => m.Id == id);
            return View(conservation);
        }

        [HttpGet("~/api/conservations/{conservationId}/groups")]
        public IEnumerable<Group> GetGroups()
        {
            var userId = _userManager.GetUserId(User);
            

            return _context.Groups.Where(q => q.Conservation.Owner == userId).ToList();
        }

        // GET api/acts/5
        [HttpGet("~/api/conservations/{conservationId}/groups/{id}")] //"api/zoos/{zooId}/exhibits/{id}
        public async Task<IActionResult> GetGroup(int conservationId) //, int id) //he only had one
        {
            

            var userId = _userManager.GetUserId(User);

            var conservation = _context.Conservations.Include(q => q.Groups).FirstOrDefault(q => q.Id == conservationId);
            var group = conservation.Groups.FirstOrDefault(q => q.Id == conservationId); //== id); //not sure if need circusId or id. He had one and hten the other soemhwere

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }



        // POST api/acts
        [HttpPost("~/api/conservations/{conservationId}/groups")]
        public async Task<IActionResult> PostGroup(int conservationId, [FromBody]Group group)
        {
            var conservation = _context.Conservations.FirstOrDefault(q => q.Id == conservationId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            group.Owner =  _userManager.GetUserId(User);

            conservation.Groups.Add(group);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (GroupExists(group.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //var fixActs = _context.Database.ExecuteSqlCommand("update Acts set Acts.CircusId = C.Id from Acts as A inner join Circuses as C on C.Owner = A.Owner");
            return CreatedAtAction("GetGroup", new { id = group.Id }, group);

        }

        // PUT api/acts/5
        [HttpPut("~/api/conservations/{conservationId}/groupss/{id}")]
        public async Task<IActionResult> PutGroup(int id, [FromBody] Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.Id)
            {
                return BadRequest();
            }

            group.Owner = _userManager.GetUserId(User);
            _context.Entry(group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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
        [HttpDelete("~/api/groups/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Group group = await _context.Groups
                .Where(q => q.Owner == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return Ok(group);

        }

        private bool GroupExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Groups.Any(e => e.Owner == userId && e.Id == id);
        }




    }
}
