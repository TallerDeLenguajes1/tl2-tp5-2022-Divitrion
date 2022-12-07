using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cadeteria.Models;
using AutoMapper;
using Cadeteria.ViewModels;
using Cadeteria.Repositorios;

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

    public IActionResult Listado()
    {
        var listadoPedidos = _repoPedidos.GetAll();
        var listadoPedidosVM = _mapper.Map<List<PedidoViewModel>>(listadoPedidos);
        foreach (var pedidoVM in listadoPedidosVM)
        {
            var cliente = _repoClientes.GetById(pedidoVM.ClienteID);
            pedidoVM.NombreCliente = cliente.Nombre;
            pedidoVM.DireccionCliente = cliente.Direccion;
        }
        return View(listadoPedidosVM);
    }

    public IActionResult AltaPedido()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CargaPedido(int CadeteId) 
    {
        var pedido = new Pedido();
        pedido.Obs = Request.Form["Observacion"];
        pedido.Estado = Request.Form["Estado"];
        pedido.CadeteID = CadeteId;
        var cliente = new Cliente();
        cliente.Nombre = Request.Form["Nombre"];
        cliente.Telefono = Request.Form["Telefono"];
        cliente.Direccion = Request.Form["Direccion"];
        _repoClientes.Create(cliente);
        pedido.Cliente = cliente;
        var listaClientes = _repoClientes.GetAll();
        pedido.ClienteID = listaClientes.Last().Id;
        _repoPedidos.Create(pedido);

        return Redirect("Listado");
    }

    [HttpGet]

    public IActionResult EditarPedido(int id, int ClienteID)
    {
        var pedido = _repoPedidos.GetById(id);
        var cliente = _repoClientes.GetById(ClienteID);
        var pedidoVM = _mapper.Map<PedidoViewModel>(pedido);
        pedidoVM.NombreCliente = cliente.Nombre;
        pedidoVM.DireccionCliente = cliente.Direccion;
        return View(pedidoVM);
    }

    [HttpPost]
    public IActionResult EditarPedido(PedidoViewModel pedidoVM)
    {
        if (ModelState.IsValid)
        {
            var pedido = _mapper.Map<Pedido>(pedidoVM);
            var cliente = _repoClientes.GetById(pedidoVM.ClienteID);
            cliente.Nombre = pedidoVM.NombreCliente;
            cliente.Direccion = pedidoVM.DireccionCliente;
            _repoPedidos.Update(pedido);
            _repoClientes.Update(cliente);
            return Redirect("Listado");
            
        }else
        {
            return View("EditarPedido");
        }
    }

    public IActionResult BorrarPedido(int id)
    {
        var pedido = _repoPedidos.GetById(id);
        _repoClientes.Delete(pedido.ClienteID);
        return Redirect("Listado");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
