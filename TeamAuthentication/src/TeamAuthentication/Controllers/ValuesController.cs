using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamAuthentication.Models;

namespace TeamAuthentication.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
       private TeamContext Context { get; set; }

        public ValuesController()
        {
            Context = new TeamContext();
        }



        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {

            var circus = Context.Circuses.Find(id);
            return circus;
        }

        // POST api/values
        [HttpPost("{id}")]
        public Circus Post(int id,[FromBody]Circus circus)
        {
            var oldCircus = Context.Circuses.Find(id);
            oldCircus.Name = circus.Name;
            Context.SaveChanges();
            return (circus);
        }

        // PUT api/values/5
        [HttpPut]
        public Circus Put([FromBody]Circus circus)
        {
           Context.Circuses.Add(circus);
            Context.SaveChanges();
            return (circus);
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(Circus circus)
        {
            Context.Circuses.Remove(circus);
            Context.SaveChanges();
        }
    }
}
