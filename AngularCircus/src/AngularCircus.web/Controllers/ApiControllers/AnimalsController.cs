using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularZoo.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AngularZoo.web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


namespace AngularZoo.web.Controllers.ApiControllers
{
    [Produces("application/json")]
    [Authorize]
    public class AnimalsController : Controller
    {
        private readonly AngularConservationContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }

        public AnimalsController(UserManager<ApplicationUser> userManager, AngularConservationContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("~/groups/{id}/animals/")]
        //[Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
        public IActionResult Animal(int id)
        {

            var group = _context.Groups.Include(q => q.Animals).FirstOrDefault(m => m.Id == id);
            return View(group);
        }

        [Route("~/api/groups/{groupId}/animals")]
        [HttpGet]
        public IEnumerable<Animal> GetAnimals()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Animals.Where(q => q.Group.Owner == userId).ToList(); //this act.Owner may be a problem
        }
        // GET api/performers/5
        [HttpGet("api/groups/{groupId}/animals/{id}")]
        public async Task<IActionResult> GetAnimal(int groupId)
        {
            

            var userId = _userManager.GetUserId(User);
            var group = _context.Groups.Include(q => q.Animals).FirstOrDefault(q => q.Id == groupId);

            var animal = group.Animals.FirstOrDefault(q => q.Id == groupId);
            //Performer Performer = await _context.Performers.SingleOrDefaultAsync(m => m.Name == userId && m.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }



        // POST api/performers
        [HttpPost("~/api/groups/{groupId}/animals")]
        public async Task<IActionResult> PostAnimal(int groupId, [FromBody]Animal animal)
        {
            var group = _context.Groups.FirstOrDefault(q => q.Id == groupId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            animal.Owner = _userManager.GetUserId(User);

            //performer.Name = _userManager.GetUserId(User);
            //_context.Performers.Add(performer);

            group.Animals.Add(animal);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (AnimalExists(animal.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        // PUT api/performers/5
        [HttpPut("~/api/groups/{groupId}/animals{id}")]
        public async Task<IActionResult> PutAnimal(int id, [FromBody] Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animal.Id)
            {
                return BadRequest();
            }

            animal.Owner = _userManager.GetUserId(User);
            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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
        [HttpDelete("~/api/animals/{id}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Animal Animal = await _context.Animals
                .Where(q => q.Owner == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (Animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(Animal);
            await _context.SaveChangesAsync();

            return Ok(Animal);

        }

        private bool AnimalExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Animals.Any(e => e.Name == userId && e.Id == id);
        }
    }

}
