using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cadeteria.Models;
using AutoMapper;
using Cadeteria.ViewModels;
using Cadeteria.Repositorios;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cadeteria.Controllers;

public class PedidosController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepositorioCadetes _repoCadetes; 
    private readonly IRepositorioPedidos _repoPedidos;
    private readonly IRepositorioClientes _repoClientes;
    private IMapper _mapper;

    public PedidosController(ILogger<HomeController> logger,IMapper mapper, IRepositorioPedidos repoPedidos, IRepositorioCadetes repoCadetes, IRepositorioClientes repoClientes)
    {
        _logger = logger;
        _mapper = mapper;
        _repoCadetes = repoCadetes;
        _repoPedidos = repoPedidos;
        _repoClientes = repoClientes;
    }

    public IActionResult Listado(int? idCadete, int? idCliente)
    {
        var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
        try
        {
            var listadoPedidos = _repoPedidos.GetAll();
            var listadoPedidosVM = new List<PedidoViewModel>();
            if (HttpContext.Session.GetInt32("CadeteID") != null)
            {
                var listadoPedidosFiltrada = listadoPedidos.Where(pedido => pedido.CadeteID == HttpContext.Session.GetInt32("CadeteID"));
                listadoPedidosVM = _mapper.Map<List<PedidoViewModel>>(listadoPedidosFiltrada);
            }else if (idCliente != null)
            {
                var listadoPedidosFiltrada = listadoPedidos.Where(pedido => pedido.ClienteID == idCliente);
                listadoPedidosVM = _mapper.Map<List<PedidoViewModel>>(listadoPedidosFiltrada);
            }else if (idCadete != null)
            {
                var listadoPedidosFiltrada = listadoPedidos.Where(pedido => pedido.CadeteID == idCadete);
                listadoPedidosVM = _mapper.Map<List<PedidoViewModel>>(listadoPedidosFiltrada);
            }else
            {
                listadoPedidosVM = _mapper.Map<List<PedidoViewModel>>(listadoPedidos);
            }
            foreach (var pedidoVM in listadoPedidosVM)
            {
                var cliente = _repoClientes.GetById(pedidoVM.ClienteID);
                pedidoVM.NombreCliente = cliente.Nombre;
                pedidoVM.DireccionCliente = cliente.Direccion;
                var cadete = _repoCadetes.GetById(pedidoVM.CadeteID);
                pedidoVM.NombreCadete = cadete.Nombre;
            }
            return View(listadoPedidosVM);
        }
        catch (System.Exception)
        {
            _logger.LogError("Error de la base de datos en la lista de Pedidos");
            return RedirectToAction("DataBaseError","Cadetes");
        }
    }

    [HttpGet]
    public IActionResult AltaPedido()
    {
        var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
        try
        {
            var listadoCadetes= _repoCadetes.GetAll();
            var itemsCadetes = listadoCadetes.ConvertAll(Cadete =>
            {
                return new SelectListItem()
                {
                    Text = Cadete.Nombre.ToString(),
                    Value = Cadete.Id.ToString()
                };
            });
            ViewBag.listadoCadetes = itemsCadetes;
            
            var listadoClientes = _repoClientes.GetAll();
            var itemsClientes = listadoClientes.ConvertAll(cliente =>
            {
                return new SelectListItem()
                {
                    Text = cliente.Nombre.ToString(),
                    Value = cliente.Id.ToString()
                };
            });
            ViewBag.listadoClientes = itemsClientes;
            return View(new PedidoViewModel());
        }
        catch (System.Exception)
        {
            _logger.LogError("Error de la base de datos en el Alta de Pedidos");
            return RedirectToAction("DataBaseError","Cadetes");
        }
    }

    [HttpPost]
    public IActionResult AltaPedido(PedidoViewModel PedidoVM) 
    {
        var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
        try
        {
            if (ModelState.IsValid)
            {
                var pedido = _mapper.Map<Pedido>(PedidoVM);
                _repoPedidos.Create(pedido);
                return RedirectToAction("Listado");
            }else
            {
                var listadoCadetes= _repoCadetes.GetAll();
                var itemsCadetes = listadoCadetes.ConvertAll(Cadete =>
                {
                    return new SelectListItem()
                    {
                        Text = Cadete.Nombre.ToString(),
                        Value = Cadete.Id.ToString()
                    };
                });
                ViewBag.listadoCadetes = itemsCadetes;
                
                var listadoClientes = _repoClientes.GetAll();
                var itemsClientes = listadoClientes.ConvertAll(cliente =>
                {
                    return new SelectListItem()
                    {
                        Text = cliente.Nombre.ToString(),
                        Value = cliente.Id.ToString()
                    };
                });
                ViewBag.listadoClientes = itemsClientes;
                return View("AltaPedido", PedidoVM);
            }
        }
        catch (System.Exception)
        {
            _logger.LogError("Error de la base de datos en el Alta de Pedidos");
            return RedirectToAction("DataBaseError","Cadetes");
        }
    }

    [HttpGet]
    public IActionResult EditarPedido(int id, int ClienteID)
    {
        var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
        try
        {
            var pedido = _repoPedidos.GetById(id);
            var cliente = _repoClientes.GetById(ClienteID);
            var pedidoVM = _mapper.Map<PedidoViewModel>(pedido);
            var listadoCadetes= _repoCadetes.GetAll();

            var itemsCadetes = listadoCadetes.ConvertAll(cadete =>
            {
                return new SelectListItem()
                {
                    Text = cadete.Nombre.ToString(),
                    Value = cadete.Id.ToString(),
                    Selected = cadete.Id == pedido.CadeteID
                };
            });
            ViewBag.listadoCadetes = itemsCadetes;
            pedidoVM.NombreCliente = cliente.Nombre;
            pedidoVM.DireccionCliente = cliente.Direccion;
            return View(pedidoVM);
        }
        catch (System.Exception)
        {
            _logger.LogError("Error de la base de datos en la Edicion de Pedidos");
            return RedirectToAction("DataBaseError","Cadetes");
        }
    }

    [HttpPost]
    public IActionResult EditarPedido(PedidoViewModel pedidoVM)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var pedido = _mapper.Map<Pedido>(pedidoVM);
                var cliente = _repoClientes.GetById(pedidoVM.ClienteID);
                cliente.Nombre = pedidoVM.NombreCliente;
                cliente.Direccion = pedidoVM.DireccionCliente;
                _repoPedidos.Update(pedido);
                _repoClientes.Update(cliente);
                return RedirectToAction("Listado");
                
            }else
            {
                var listadoCadetes= _repoCadetes.GetAll();
                var itemsCadetes = listadoCadetes.ConvertAll(Cadete =>
                {
                    return new SelectListItem()
                    {
                        Text = Cadete.Nombre.ToString(),
                        Value = Cadete.Id.ToString()
                    };
                });
                ViewBag.listadoCadetes = itemsCadetes;
                return View("EditarPedido", pedidoVM);
            }
        }
        catch (System.Exception)
        {
           _logger.LogError("Error de la base de datos en la Edicion de Pedidos");
            return RedirectToAction("DataBaseError","Cadetes");
        }
    }

    public IActionResult BorrarPedido(int id)
    {
        var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
        try
        {
            var pedido = _repoPedidos.GetById(id);
            _repoPedidos.Delete(pedido.Nro);
            return Redirect("Listado");
        }
        catch (System.Exception)
        {
            _logger.LogError("Error de la base de datos en la Edicion de Pedidos");
            return RedirectToAction("DataBaseError","Cadetes");
        }
    }

    public IActionResult BajaPedido(int id)
    {
        var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null)
            {
               return RedirectToAction("Login","Login");
            }
        try
        {
            var pedido = _repoPedidos.GetById(id);
            PedidoViewModel pedidoVM = _mapper.Map<PedidoViewModel>(pedido);
            var cliente = _repoClientes.GetById(pedidoVM.ClienteID);
            pedidoVM.NombreCliente = cliente.Nombre;
            pedidoVM.DireccionCliente = cliente.Direccion;
            var cadete = _repoCadetes.GetById(pedidoVM.CadeteID);
            pedidoVM.NombreCadete = cadete.Nombre;
            return View(pedidoVM);
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
