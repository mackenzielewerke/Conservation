using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Conservation.web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid Signature { get; set; }
    }
}
