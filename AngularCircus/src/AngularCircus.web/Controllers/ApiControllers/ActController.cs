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
    public class ActController : Controller
    {
        public AngularCircusContext Context { get; set; }

        public ActController()
        {
            Context = new AngularCircusContext();
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<Act> Get()
        {
            return Context.Acts;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Act Get(int id)
        {
            return Context.Acts.First(q => q.Id == id);
        }

        // POST api/values
        [HttpPost]
        public Act Post([FromBody]Act value)
        {
            Context.Acts.Add(value);
            Context.SaveChanges();
            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Act Put(int id, [FromBody]Act value)
        {
            var existing = Context.Acts.First(q => q.Id == id);
            existing.Name = value.Name;
            Context.SaveChanges();
            return value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existing = Context.Acts.First(q => q.Id == id);
            Context.Acts.Remove(existing);
            Context.SaveChanges();
        }
    }
}
