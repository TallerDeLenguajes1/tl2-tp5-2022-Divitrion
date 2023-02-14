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
        private readonly IRepositorioPedidos _repoPedidos;
        private readonly IRepositorioUsuarios _repoUsuarios; 
        private IMapper _mapper;

        public CadetesController(ILogger<CadetesController> logger, IMapper mapper, IRepositorioCadetes repoCadetes, IRepositorioUsuarios repoUsuarios,IRepositorioPedidos repoPedidos)
        {
            _logger = logger;
            _mapper = mapper;
            _repoCadetes = repoCadetes;
            _repoUsuarios = repoUsuarios;
            _repoPedidos = repoPedidos;
        }

        public IActionResult Listado()
        {
            var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
            if (rol == 2)
            {
                return RedirectToAction("UserError","Login");
            }
            try
            {
                var listado = _repoCadetes.GetAll();
                var listadoVM = _mapper.Map<List<CadeteViewModel>>(listado);
                foreach (var cadeteVM in listadoVM)
                {
                    cadeteVM.listadoPedidos = _repoCadetes.GetPedidos(cadeteVM.Id);
                }
                return View(listadoVM);
            }
            catch (System.Exception)
            {
                _logger.LogError("Error de la base de datos en el listado de Cadetes");
                return RedirectToAction("DataBaseError");
            }
        }

        [HttpGet]
        public IActionResult EditarCadete(int idCadete)
        {
            var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
            if (rol == 2)
            {
                return RedirectToAction("UserError","Login");
            }
            try
            {
                var cadeteEditable = _repoCadetes.GetById(idCadete);
                var cadeteEditableVM = _mapper.Map<CadeteViewModel>(cadeteEditable);
                return View(cadeteEditableVM);
            }
            catch (System.Exception)
            {
                _logger.LogError("Error de la base de datos en la edicion de Cadetes");
                return RedirectToAction("DataBaseError");
            }
        }

        [HttpPost]

        public IActionResult EditarCadete(CadeteViewModel cadeteVM)
        {
            try
            {
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
            catch (System.Exception)
            {
                _logger.LogError("Error de la base de datos en la edicion de Cadetes");
                return RedirectToAction("DataBaseError");
            }
        }

        public IActionResult AltaCadete()
        {
            var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
            if (rol == 2)
            {
                return RedirectToAction("UserError","Login");
            }
            try
            {
                return View(new CadeteViewModel());
            }
            catch (System.Exception)
            {
                _logger.LogError("Error de la base de datos en la Alta de Cadetes");
                return RedirectToAction("DataBaseError");
            }
        }

        [HttpPost]
        public IActionResult AltaCadete(CadeteViewModel cadeteVM)
        {
            try
            {
                 if (ModelState.IsValid)
                {
                    var cadete = _mapper.Map<Cadete>(cadeteVM);
                    _repoUsuarios.CreateUser(cadete.Nombre,2,"Cdt"+cadete.Nombre, cadete.Telefono);
                    cadete.UserId = _repoUsuarios.getUser("Cdt"+cadete.Nombre, cadete.Telefono).Id;
                    _repoCadetes.Create(cadete);
                    return RedirectToAction("Listado");
                }else
                {
                    return View("AltaCadete", cadeteVM);
                }
            }
            catch (System.Exception)
            {
                _logger.LogError("Error de la base de datos en la Alta de Cadetes");
                return RedirectToAction("DataBaseError");
            }
        }

        public IActionResult BorrarCadete(int id)
        {
            var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
            if (rol == 2)
            {
                return RedirectToAction("UserError","Login");
            }
            try
            {
                var cadetePedidos = _repoCadetes.GetPedidos(id);
                foreach (var pedido in cadetePedidos)
                {
                    pedido.CadeteID = 0;
                    pedido.Estado = "No Asignado";
                    _repoPedidos.Update(pedido);
                }
                _repoUsuarios.Delete(_repoCadetes.GetById(id).UserId);
                _repoCadetes.Delete(id);
                return Redirect("Listado");
                
            }
            catch (System.Exception)
            {
                _logger.LogError("Error de la base de datos en la Baja de Cadetes");
                return RedirectToAction("DataBaseError");
            }
        }

        public IActionResult BajaCadete(int id)
        {
            var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
            if (rol == 2)
            {
                return RedirectToAction("UserError","Login");
            }
            try
            {
                var cadete = _repoCadetes.GetById(id);
                CadeteViewModel cadeteVM = _mapper.Map<CadeteViewModel>(cadete);
                return View(cadeteVM);
            }
            catch (System.Exception)
            {
                _logger.LogError("Error de la base de datos en la Edicion de Pedidos");
                return RedirectToAction("DataBaseError","Cadetes");
            }
        }

        public IActionResult DataBaseError()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}