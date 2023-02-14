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
        private readonly ILogger<ClientesController> _logger;
        private readonly IRepositorioClientes _repoClientes;
        private readonly IRepositorioPedidos _repoPedidos;
        private IMapper _mapper;

        public ClientesController(ILogger<ClientesController> logger, IMapper mapper, IRepositorioClientes repoClientes, IRepositorioPedidos repoPedidos)
        {
            _mapper = mapper;
            _repoClientes = repoClientes;
            _logger = logger;
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
                var listado = _repoClientes.GetAll();
                var listadoVM = _mapper.Map<List<ClienteViewmodel>>(listado);
                return View(listadoVM);
            }
            catch (System.Exception)
            {
                
               _logger.LogError("Error de la base de datos en el listado de Clientes");
                return RedirectToAction("DataBaseError","Cadetes");
            }
        }

        [HttpGet]
        public IActionResult EditarCliente(int idCliente)
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
                var clienteEditable = _repoClientes.GetById(idCliente);
                var clienteEditableVM = _mapper.Map<ClienteViewmodel>(clienteEditable);
                return View(clienteEditableVM);
            }
            catch (System.Exception)
            {
                 _logger.LogError("Error de la base de datos en la edicion de Clientes");
                return RedirectToAction("DataBaseError","Cadetes");
            }
        }

        [HttpPost]

        public IActionResult EditarCliente(ClienteViewmodel clienteVM)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteVM);
                _repoClientes.Update(cliente);
                return Redirect("Listado");
            }
            catch (System.Exception)
            {
               _logger.LogError("Error de la base de datos en la edicion de Clientes");
                return RedirectToAction("DataBaseError","Cadetes");
            }
        }

        public IActionResult AltaCliente()
        {
            var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
            try
            {
                return View(new ClienteViewmodel());
            }
            catch (System.Exception)
            {
                _logger.LogError("Error de la base de datos en el Alta de Clientes");
                return RedirectToAction("DataBaseError","Cadetes");
            }
        }

        [HttpPost]
        public IActionResult AltaCliente(ClienteViewmodel clienteVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = _mapper.Map<Cliente>(clienteVM);
                    _repoClientes.Create(cliente);
                    if (HttpContext.Session.GetInt32("Rol")==2)
                    {
                        return RedirectToAction("AltaPedido","Pedidos");
                    }
                    return RedirectToAction("Listado");
                }else
                {
                    return View("AltaCliente", clienteVM);
                }
            }
            catch (System.Exception)
            {
               _logger.LogError("Error de la base de datos en el Alta de Clientes");
                return RedirectToAction("DataBaseError","Cadetes");
            }
        }

        public IActionResult BorrarCliente(int id)
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
                var clientePedidos = _repoClientes.GetPedidos(id);
                foreach (var pedido in clientePedidos)
                {
                    _repoPedidos.Delete(pedido.Nro);
                }
                _repoClientes.Delete(id);
                return Redirect("Listado");
            }
            catch (System.Exception)
            {
                _logger.LogError("Error de la base de datos en la baja de Clientes");
                return RedirectToAction("DataBaseError","Cadetes");
            }
        }

        public IActionResult BajaCliente(int id)
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
                var cliente = _repoClientes.GetById(id);
                ClienteViewmodel clienteVM = _mapper.Map<ClienteViewmodel>(cliente);
                return View(clienteVM);
            }
            catch (System.Exception)
            {
                _logger.LogError("Error de la base de datos en la Edicion de Pedidos");
                return RedirectToAction("DataBaseError","Cadetes");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}