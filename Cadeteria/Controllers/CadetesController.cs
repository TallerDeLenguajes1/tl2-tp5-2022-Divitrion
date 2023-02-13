using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cadeteria.Models;
using AutoMapper;
using Cadeteria.ViewModels;
using Cadeteria.Repositorios;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authorization;

namespace Cadeteria.Controllers
{
    public class CadetesController : Controller
    {
        private readonly ILogger<CadetesController> _logger;
        private readonly IRepositorioCadetes _repoCadetes;
        private readonly IRepositorioUsuarios _repoUsuarios; 
        private IMapper _mapper;

        public CadetesController(ILogger<CadetesController> logger, IMapper mapper, IRepositorioCadetes repoCadetes, IRepositorioUsuarios repoUsuarios)
        {
            _logger = logger;
            _mapper = mapper;
            _repoCadetes = repoCadetes;
            _repoUsuarios = repoUsuarios;
        }

        public IActionResult Listado()
        {
            if (HttpContext.Session.GetInt32("Rol") == null)
            {
               return RedirectToAction("Login","Login");
            }
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
            if (HttpContext.Session.GetInt32("Rol") == null || HttpContext.Session.GetInt32("Rol") == 2)
            {
               return RedirectToAction("Login","Login");
            }
            var cadeteEditable = _repoCadetes.GetById(idCadete);
            var cadeteEditableVM = _mapper.Map<CadeteViewModel>(cadeteEditable);
            return View(cadeteEditableVM);
        }

        [HttpPost]

        public IActionResult EditarCadete(CadeteViewModel cadeteVM)
        {
            if (HttpContext.Session.GetInt32("Rol") == null || HttpContext.Session.GetInt32("Rol") == 2)
            {
                return RedirectToAction("Login","Login");
            }
            if (ModelState.IsValid)
            {
                var cadete = _mapper.Map<Cadete>(cadeteVM);
                _repoCadetes.Update(cadete);
                return RedirectToAction("Listado");
            }else
            {
                return View("EditarCadete", cadeteVM);
            }
        }

        public IActionResult AltaCadete()
        {
            if (HttpContext.Session.GetInt32("Rol") == null || HttpContext.Session.GetInt32("Rol") == 2)
            {
               return RedirectToAction("Login","Login");
            }
            return View(new CadeteViewModel());
        }

        [HttpPost]
        public IActionResult AltaCadete(CadeteViewModel cadeteVM)
        {
            if (HttpContext.Session.GetInt32("Rol") == null || HttpContext.Session.GetInt32("Rol") == 2)
            {
               return RedirectToAction("Login","Login");
            }
            if (ModelState.IsValid)
            {
                var cadete = _mapper.Map<Cadete>(cadeteVM);
                _repoUsuarios.CreateUser(cadete.Nombre,2,"Cdt"+cadete.Nombre);
                cadete.UserId = _repoUsuarios.getUser("Cdt"+cadete.Nombre,"Cdt"+cadete.Nombre).Id;
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
            if (HttpContext.Session.GetInt32("Rol") == null || HttpContext.Session.GetInt32("Rol") == 2)
            {
               return RedirectToAction("Login","Login");
            }
            _repoUsuarios.Delete(_repoCadetes.GetById(id).UserId);
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