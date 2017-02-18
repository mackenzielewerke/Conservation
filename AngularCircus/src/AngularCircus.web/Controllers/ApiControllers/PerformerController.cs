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
    public class PerformerController : Controller
    {
        public AngularCircusContext Context { get; set; }

        public PerformerController()
        {
            Context = new AngularCircusContext();
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<Performer> Get()
        {
            return Context.Performers;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Performer Get(int id)
        {
            return Context.Performers.First(q => q.Id == id);
        }

        // POST api/values
        [HttpPost]
        public Performer Post([FromBody]Performer value)
        {
            Context.Performers.Add(value);
            Context.SaveChanges();
            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Performer Put(int id, [FromBody]Performer value)
        {
            var existing = Context.Performers.First(q => q.Id == id);
                existing.Name = value.Name;
            Context.SaveChanges();
            return value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existing = Context.Performers.First(q => q.Id == id);
            Context.Performers.Remove(existing);
            Context.SaveChanges();
        }
    }
}
