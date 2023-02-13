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
    public class ClientesController : Controller
    {
        private readonly ILogger<CadetesController> _logger;
        private readonly IRepositorioClientes _repoClientes;
        private IMapper _mapper;

        public ClientesController(ILogger<ClientesController> logger, IMapper mapper, IRepositorioClientes repoClientes)
        {
            _mapper = mapper;
            _repoClientes = repoClientes;
        }

        public IActionResult Listado()
        {
            if (HttpContext.Session.GetInt32("Rol") == null)
            {
               return RedirectToAction("Login","Login");
            }
            var listado = _repoClientes.GetAll();
            var listadoVM = _mapper.Map<List<ClienteViewmodel>>(listado);
            return View(listadoVM);
        }

        [HttpGet]
        public IActionResult EditarCliente(int idCliente)
        {
            if (HttpContext.Session.GetInt32("Rol") == null || HttpContext.Session.GetInt32("Rol") == 2)
            {
               return RedirectToAction("Login","Login");
            }
            var clienteEditable = _repoClientes.GetById(idCliente);
            var clienteEditableVM = _mapper.Map<ClienteViewmodel>(clienteEditable);
            return View(clienteEditableVM);
        }

        [HttpPost]

        public IActionResult EditarCliente(ClienteViewmodel clienteVM)
        {
            if (HttpContext.Session.GetInt32("Rol") == null || HttpContext.Session.GetInt32("Rol") == 2)
            {
                return RedirectToAction("Login","Login");
            }
            var cliente = _mapper.Map<Cliente>(clienteVM);
            _repoClientes.Update(cliente);
            return Redirect("Listado");
        }

        public IActionResult AltaCliente()
        {
            if (HttpContext.Session.GetInt32("Rol") == null || HttpContext.Session.GetInt32("Rol") == 2)
            {
               return RedirectToAction("Login","Login");
            }
            return View(new ClienteViewmodel());
        }

        [HttpPost]
        public IActionResult AltaCliente(ClienteViewmodel clienteVM)
        {
            if (HttpContext.Session.GetInt32("Rol") == null || HttpContext.Session.GetInt32("Rol") == 2)
            {
               return RedirectToAction("Login","Login");
            }
            if (ModelState.IsValid)
            {
                var cliente = _mapper.Map<Cliente>(clienteVM);
                _repoClientes.Create(cliente);
                return RedirectToAction("Listado");
            }else
            {
                return View("AltaCliente", clienteVM);
            }
        }

        [HttpGet]
        public IActionResult BorrarCadete(int id)
        {
            if (HttpContext.Session.GetInt32("Rol") == null || HttpContext.Session.GetInt32("Rol") == 2)
            {
               return RedirectToAction("Login","Login");
            }
            _repoClientes.Delete(id);
            return Redirect("Listado");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}