using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Models;
using Cadeteria.ViewModels;

namespace Cadeteria.Controllers
{
    public class CadetesController : Controller
    {
        private readonly ILogger<CadetesController> _logger;
        private 

        public CadetesController(ILogger<CadetesController> logger)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Listado()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Carga()
        {

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}