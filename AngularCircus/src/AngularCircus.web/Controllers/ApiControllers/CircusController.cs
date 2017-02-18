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
    public class CircusController : Controller
    {
        public AngularCircusContext Context { get; set; }

        public CircusController()
        {
            Context = new AngularCircusContext();
        }
       

        // GET: api/values
        [HttpGet]
        public IEnumerable<Circus> Get()
        {
            return Context.Circuses;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Circus Get(int id)
        {
            return Context.Circuses.First(q=>q.Id == id);
        }

        // POST api/values
        [HttpPost]
        public Circus Post([FromBody]Circus value)
        {
            Context.Circuses.Add(value);
            Context.SaveChanges();
            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Circus Put(int id, [FromBody]Circus value)
        {
            var existing = Context.Circuses.First(q => q.Id == id);
            existing.IsDone = value.IsDone;
            existing.Name = value.Name;
            Context.SaveChanges();
            return value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existing = Context.Circuses.First(q => q.Id == id);
            Context.Circuses.Remove(existing);
            Context.SaveChanges();
        }
    }
}
