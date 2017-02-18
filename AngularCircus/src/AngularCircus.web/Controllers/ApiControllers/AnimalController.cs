using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularCircus.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularCircus.web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class AnimalController : Controller
    {
        public AngularCircusContext Context { get; set; }

        public AnimalController()
        {
            Context = new AngularCircusContext();
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<Animal> Get()
        {
            return Context.Animals;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Animal Get(int id)
        {
            return Context.Animals.First(q => q.Id == id);
        }

        // POST api/values
        [HttpPost]
        public Animal Post([FromBody]Animal value)
        {
            Context.Animals.Add(value);
            Context.SaveChanges();
            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Animal Put(int id, [FromBody]Animal value)
        {
            var existing = Context.Animals.First(q => q.Id == id);
            existing.Name = value.Name;
            Context.SaveChanges();
            return value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existing = Context.Animals.First(q => q.Id == id);
            Context.Animals.Remove(existing);
            Context.SaveChanges();
        }
    }
}
