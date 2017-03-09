using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AngularZoo.web.Models;

namespace AngularZoo.web.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Guid Signature { get; set; }
    }

}
