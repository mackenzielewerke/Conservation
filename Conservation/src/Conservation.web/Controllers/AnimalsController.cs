using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conservation.web.Controllers
{
    [Produces("application/json")]
    [Route("api/Animals")]
    public class AnimalsController : Controller
    {
    }
}