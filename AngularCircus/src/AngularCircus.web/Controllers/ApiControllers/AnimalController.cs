using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AngularCircus.web.Models;
using Microsoft.AspNetCore.Identity;
using AngularCircus.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


namespace AngularCircus.web.Controllers.ApiControllers
{
    [Produces("application/json")]
    [Authorize]
    public class AnimalController : Controller
    {
        private readonly AngularCircusContext _context;

        private UserManager<ApplicationUser> _userManager { get; set; }
        public AngularCircusContext Context { get; set; }

        public AnimalController(UserManager<ApplicationUser> userManager, AngularCircusContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("~/animal")]
        [Authorize(ActiveAuthenticationSchemes = "Identity.Application")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("~/api/animals")]
        [HttpGet]
        public IEnumerable<Animal> GetAnimals()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Animals.Where(q => q.Name == userId).ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimals([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);
            Animal Animal = await _context.Animals
                .SingleOrDefaultAsync(m => m.Name == userId && m.Id == id);

            if (Animal == null)
            {
                return NotFound();
            }

            return Ok(Animal);
        }



        // POST api/performers
        [HttpPost]
        public async Task<IActionResult> PostAnimal([FromBody]Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            animal.Name = _userManager.GetUserId(User);
            _context.Animals.Add(animal);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal([FromBody]int id, [FromBody] Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animal.Id)
            {
                return BadRequest();
            }

            animal.Name = _userManager.GetUserId(User);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Animal Animal = await _context.Animals
                .Where(q => q.Name == userId)
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
