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
    public class CircusController : Controller
    {
        public AngularCircusContext Context { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }


        private readonly AngularCircusContext _context;
        public CircusController(UserManager<ApplicationUser> userManager, AngularCircusContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<Circus> Get()
        {
            return _context.Circuses;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Circus Get(int id)
        {
            return _context.Circuses.First(q => q.Id == id);
        }

        // POST api/values
        [HttpPost]
        public Circus Post([FromBody]Circus value)
        {
            _context.Circuses.Add(value);
            _context.SaveChanges();
            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Circus Put(int id, [FromBody]Circus value)
        {
            var existing = _context.Circuses.First(q => q.Id == id);
            existing.IsDone = value.IsDone;
            existing.Name = value.Name;
            _context.SaveChanges();
            return value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existing = _context.Circuses.First(q => q.Id == id);
            _context.Circuses.Remove(existing);
            _context.SaveChanges();
        }
    }
}
