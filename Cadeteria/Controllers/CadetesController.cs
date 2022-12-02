using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cadeteria.Models;
using AutoMapper;
using Cadeteria.ViewModels;
using Cadeteria.Repositorios;

namespace Cadeteria.Controllers
{
    public class CadetesController : Controller
    {
        private readonly ILogger<CadetesController> _logger;
        private readonly IRepositorioCadetes _repositorio; 
        private IMapper _mapper;

        public CadetesController(ILogger<CadetesController> logger, IMapper mapper, IRepositorioCadetes repositorio)
        {
            _logger = logger;
            _mapper = mapper;
            _repositorio = repositorio;
        }

        public IActionResult Listado()
        {
            var listado = _repositorio.GetAll();
            var listadoVM = _mapper.Map<List<CadeteViewModel>>(listado);
            return View(listadoVM);
        }

        [HttpPost]
        // public IActionResult Carga()
        // {

        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}