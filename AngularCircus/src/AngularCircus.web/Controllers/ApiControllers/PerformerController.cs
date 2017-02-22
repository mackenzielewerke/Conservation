using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngularCircus.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AngularCircus.web.Data;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularCircus.web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class PerformerController : Controller
    {
        public AngularCircusContext Context { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }


        private readonly AngularCircusContext _context;
        public PerformerController(UserManager<ApplicationUser> userManager, AngularCircusContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Performer> Get()
        {
            return _context.Performers;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Performer Get(int id)
        {
            return _context.Performers.First(q => q.Id == id);
        }

        // POST api/values
        [HttpPost]
        public Performer Post([FromBody]Performer value)
        {
            _context.Performers.Add(value);
            _context.SaveChanges();
            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Performer Put(int id, [FromBody]Performer value)
        {
            var existing = _context.Performers.First(q => q.Id == id);
                existing.Name = value.Name;
            _context.SaveChanges();
            return value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existing = _context.Performers.First(q => q.Id == id);
            _context.Performers.Remove(existing);
            _context.SaveChanges();
        }
    }
}
