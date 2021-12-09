using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PecuaristaController : ControllerBase
    {
        // GET: api/<PecuaristaController>
        [HttpGet]
        public IEnumerable<Pecuarista> Get()
        {
            return GadoServices.GetAllPecuarista();
        }
    }
}
