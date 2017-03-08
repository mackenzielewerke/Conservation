using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularCircus.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AngularCircus.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;



namespace AngularCircus.web.Controllers.ApiControllers
{
    [Produces("application/json")]
    //[Route("~/circus")]
    [Authorize]
    public class ZoosController : Controller
    {
        private readonly AngularZooContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }
        

        public ZoosController(UserManager<ApplicationUser> userManager, AngularZooContext context)
        {
            _userManager = userManager;
            _context = context;

        }

        [Route("~/zoos/")]
        [Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
        public IActionResult Zoo()
        {
            return View();
        }

        [HttpGet("~/api/zoos")]
        public IEnumerable<Zoo> GetZoos()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Zoos.Where(q => q.Owner == userId).ToList();
        }

        // GET api/circuses/5
        [HttpGet("~/api/zoos/{id}")]
        public async Task<IActionResult> GetZoo(int id)
        {

            var userId = _userManager.GetUserId(User);
            Zoo zoo = await _context.Zoos
                .SingleOrDefaultAsync(m => m.Owner == userId && m.Id == id);

            if (zoo == null)
            {
                return NotFound();
            }

            return Ok(zoo);
        }



        // POST api/circuses
        [HttpPost("~/api/zoos")]
        public async Task<IActionResult> PostZoo([FromBody]Zoo zoo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            zoo.Owner = _userManager.GetUserId(User);
            _context.Zoos.Add(zoo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (ZooExists(zoo.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetCircus", new { id = zoo.Id }, zoo);
        }

        // PUT api/circuses/5
        [HttpPut("~/api/zoos/{id}")]
        public async Task<IActionResult> PutZoo(int id, [FromBody] Zoo zoo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zoo.Id)
            {
                return BadRequest();
            }

            zoo.Owner = _userManager.GetUserId(User);
            _context.Entry(zoo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ZooExists(id))
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
        [HttpDelete("~/api/zoos/{id}")]
        public async Task<IActionResult> DeleteZoo(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Zoo zoo = await _context.Zoos
                .Where(q => q.Owner == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if(zoo == null)
            {
                return NotFound();
            }

            _context.Zoos.Remove(zoo);
            await _context.SaveChangesAsync();

            return Ok(zoo);
        }
        
        private bool ZooExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Zoos.Any(e => e.Owner == userId && e.Id == id);
        }
    }
}
