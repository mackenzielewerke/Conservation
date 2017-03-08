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
    public class AnimalsController : Controller
    {
        private readonly AngularZooContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }

        public AnimalsController(UserManager<ApplicationUser> userManager, AngularZooContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("~/exhibits/{id}/animals/")]
        //[Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
        public IActionResult Animal(int id)
        {

            var act = _context.Exhibits.Include(q => q.Animals).FirstOrDefault(m => m.Id == id);
            return View(act);
        }

        [Route("~/api/exhibits/{exhibitId}/animals")]
        [HttpGet]
        public IEnumerable<Animal> GetAnimals()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Animals.Where(q => q.Exhibit.Owner == userId).ToList(); //this act.Owner may be a problem
        }
        // GET api/performers/5
        [HttpGet("api/exhibits/{exhibitId}/animals/{id}")]
        public async Task<IActionResult> GetAnimal(int exhibitId)
        {
            

            var userId = _userManager.GetUserId(User);
            var exhibit = _context.Exhibits.Include(q => q.Animals).FirstOrDefault(q => q.Id == exhibitId);

            var animal = exhibit.Animals.FirstOrDefault(q => q.Id == exhibitId);
            //Performer Performer = await _context.Performers.SingleOrDefaultAsync(m => m.Name == userId && m.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }



        // POST api/performers
        [HttpPost("~/api/exhibits/{exhibitId}/animals")]
        public async Task<IActionResult> PostAnimal(int exhibitId, [FromBody]Animal animal)
        {
            var exhibit = _context.Exhibits.FirstOrDefault(q => q.Id == exhibitId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            animal.Owner = _userManager.GetUserId(User);

            //performer.Name = _userManager.GetUserId(User);
            //_context.Performers.Add(performer);

            exhibit.Animals.Add(animal);
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
        [HttpPut("~/api/exhibits/{exhibitId}/animals{id}")]
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
