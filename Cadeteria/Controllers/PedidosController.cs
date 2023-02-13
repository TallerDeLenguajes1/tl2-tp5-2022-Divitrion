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

    public IActionResult Listado(int? id)
    {
        if (HttpContext.Session.GetInt32("Rol") == null)
            {
               return RedirectToAction("Login","Login");
            }
        var listadoPedidos = _repoPedidos.GetAll();
        var listadoPedidosVM = new List<PedidoViewModel>();
        if (id != null)
        {
            var listadoPedidosFiltrada = listadoPedidos.Where(pedido => pedido.CadeteID == id);
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
        }
        return View(listadoPedidosVM);
    }

    [HttpGet]
    public IActionResult AltaPedido()
    {
        if (HttpContext.Session.GetInt32("Rol") == null)
            {
               return RedirectToAction("Login","Login");
            }
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

    [HttpPost]
    public IActionResult AltaPedido(PedidoViewModel PedidoVM) 
    {
        if (HttpContext.Session.GetInt32("Rol") == null)
            {
               return RedirectToAction("Login","Login");
            }
        
        if (ModelState.IsValid)
        {
            var pedido = _mapper.Map<Pedido>(PedidoVM);
            _repoPedidos.Create(pedido);
            return RedirectToAction("Listado");
        }else
        {
            // var listadoCadetes= _repoCadetes.GetAll();
            // var itemsCadetes = listadoCadetes.ConvertAll(Cadete =>
            // {
            //     return new SelectListItem()
            //     {
            //         Text = Cadete.Nombre.ToString(),
            //         Value = Cadete.Id.ToString()
            //     };
            // });
            // ViewBag.listadoCadetes = itemsCadetes;
            
            // var listadoClientes = _repoClientes.GetAll();
            // var itemsClientes = listadoClientes.ConvertAll(cliente =>
            // {
            //     return new SelectListItem()
            //     {
            //         Text = cliente.Nombre.ToString(),
            //         Value = cliente.Id.ToString()
            //     };
            // });
            // ViewBag.listadoClientes = itemsClientes;
            return View("AltaPedido", PedidoVM);
        }

    }

    [HttpGet]
    public IActionResult EditarPedido(int id, int ClienteID)
    {
        if (HttpContext.Session.GetInt32("Rol") == null)
            {
               return RedirectToAction("Login","Login");
            }
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

    [HttpPost]
    public IActionResult EditarPedido(PedidoViewModel pedidoVM)
    {
        if (HttpContext.Session.GetInt32("Rol") == null)
            {
               return RedirectToAction("Login","Login");
            }
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
            // var listadoCadetes= _repoCadetes.GetAll();
            // var itemsCadetes = listadoCadetes.ConvertAll(Cadete =>
            // {
            //     return new SelectListItem()
            //     {
            //         Text = Cadete.Nombre.ToString(),
            //         Value = Cadete.Id.ToString()
            //     };
            // });
            // ViewBag.listadoCadetes = itemsCadetes;
            return View("EditarPedido", pedidoVM);
        }
    }

    public IActionResult BorrarPedido(int id)
    {
        if (HttpContext.Session.GetInt32("Rol") == null)
            {
               return RedirectToAction("Login","Login");
            }
        var pedido = _repoPedidos.GetById(id);
        _repoPedidos.Delete(pedido.Nro);
        return Redirect("Listado");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
