using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ruleta_Api.Model;

namespace Ruleta_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouletteController : ControllerBase
    {
      
        private readonly ILogger<RouletteController> _logger;

        public RouletteController(ILogger<RouletteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Roulette> Get()
        {
            return null;
        }
    }
}
