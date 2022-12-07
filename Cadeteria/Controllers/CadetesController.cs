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
        private readonly IRepositorioCadetes _repoCadetes; 
        private IMapper _mapper;

        public CadetesController(ILogger<CadetesController> logger, IMapper mapper, IRepositorioCadetes repoCadetes)
        {
            _logger = logger;
            _mapper = mapper;
            _repoCadetes = repoCadetes;
        }

        public IActionResult Listado()
        {
            var listado = _repoCadetes.GetAll();
            var listadoVM = _mapper.Map<List<CadeteViewModel>>(listado);
            foreach (var cadeteVM in listadoVM)
            {
                cadeteVM.listadoPedidos = _repoCadetes.GetPedidos(cadeteVM.Id);
            }
            return View(listadoVM);
        }

        [HttpGet]
        public IActionResult EditarCadete(int idCadete)
        {
            var cadeteEditable = _repoCadetes.GetById(idCadete);
            var cadeteEditableVM = _mapper.Map<CadeteViewModel>(cadeteEditable);
            return View(cadeteEditableVM);
        }

        [HttpPost]

        public IActionResult EditarCadete(CadeteViewModel cadeteVM)
        {
            var cadete = _mapper.Map<Cadete>(cadeteVM);
            _repoCadetes.Update(cadete);
            return Redirect("Listado");
        }

        public IActionResult AltaCadete()
        {
            return View(new CadeteViewModel());
        }

        [HttpPost]
        public IActionResult AltaCadete(CadeteViewModel cadeteVM)
        {
            if (ModelState.IsValid)
            {
                var cadete = _mapper.Map<Cadete>(cadeteVM);
                _repoCadetes.Create(cadete);
                return RedirectToAction("Listado");
            }else
            {
                return View("AltaCadete", cadeteVM);
            }
        }

        [HttpGet]
        public IActionResult BorrarCadete(int id)
        {
            _repoCadetes.Delete(id);
            return Redirect("Listado");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}