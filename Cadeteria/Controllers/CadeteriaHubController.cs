using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cadeteria.Models;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;

namespace Cadeteria.Controllers;

public class CadeteriaHubController : Controller
{
    private readonly ILogger<CadeteriaHubController> _logger;
    private static CadeteriaWeb cadeteria = new CadeteriaWeb();
    private static List<PedidoViewModel> listaViewModel = new List<PedidoViewModel>();

    static string connectionString = "Data Source=DB/PedidosDB.db;Cache=Shared";
    SqliteConnection connection = new SqliteConnection(connectionString);
    SqliteDataReader lector;


    public CadeteriaHubController(ILogger<CadeteriaHubController> logger)
    {
        _logger = logger;
    }

    [HttpPost]

    public IActionResult Carga(string nombre)
    {
        cadeteria.AgregarCadete(Request.Form["Nombre"],Request.Form["Telefono"],Request.Form["Direccion"]);
        return Redirect("Listado");
    }

    [HttpGet]
    public IActionResult Borrar(int id)
    {
        cadeteria.RemoverCadete(id);
        return Redirect("listado");
    }

    public IActionResult Listado()
    {
        return View(cadeteria.ListadoCadetes);
    }

    public IActionResult AltaCadete()
    {
        return View();
    }

    public IActionResult BajaCadete()
    {
        return View(cadeteria.ListadoCadetes);
    }

//Pedidos

    public Pedido CrearPedido(string obs, string nombre, string telefono, string direccion)
    {
        var pedido = new Pedido();
        pedido.Obs = obs;
        pedido.Cliente.Direccion = direccion;
        pedido.Cliente.Nombre = nombre;
        pedido.Cliente.Telefono = telefono;        
        return pedido;
    }

    public void ViewModelMapper(Cadete cadete, Pedido pedido)
    {
        var ViewModel = new PedidoViewModel();
        ViewModel.Pedido = pedido;
        ViewModel.Cadete = cadete;
        listaViewModel.Add(ViewModel);
    }

    [HttpPost]

    public IActionResult CargaPedido()
    {
        var Pedido = CrearPedido(Request.Form["Observacion"],Request.Form["Nombre"],Request.Form["Telefono"],Request.Form["Direccion"]);
        var CadeteARecibir = cadeteria.ListadoCadetes[Convert.ToInt32(Request.Form["cadete"])];
        CadeteARecibir.recibirPedido(Pedido);
        ViewModelMapper(CadeteARecibir, Pedido);
        return Redirect("ListadoPedidos");
    }

    public IActionResult BorrarPedido(int id)
    {
        var PedidoABorrar = listaViewModel.Find(viewModel => viewModel.Pedido.Nro == id);
        var cadeteConPedido = cadeteria.ListadoCadetes.Find(cadete => cadete.listadoPedidos.Any(pedido => pedido.Nro == id));

        cadeteConPedido.listadoPedidos.Remove(PedidoABorrar.Pedido);
        listaViewModel.Remove(PedidoABorrar);
        return Redirect("ListadoPedidos");
    }

    
    [HttpGet]
    public IActionResult EditPedido(int id)
    {
        var viewModel = listaViewModel.Find(viewModel => viewModel.Pedido.Nro == id);
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult EditarPedido(int id)
    {
        var viewModel = listaViewModel.Find(viewModel => viewModel.Pedido.Nro == id);
        viewModel.Pedido.Obs = Request.Form["Observacion"];
        viewModel.Pedido.Cliente.Nombre = Request.Form["Nombre"];
        viewModel.Pedido.Cliente.Direccion = Request.Form["Direccion"];
        viewModel.Pedido.Cliente.Telefono = Request.Form["Telefono"];
        viewModel.Pedido.Estado = Request.Form["Estado"];

        var CadeteARecibir = cadeteria.ListadoCadetes[Convert.ToInt32(Request.Form["cadete"])];
        CadeteARecibir.recibirPedido(viewModel.Pedido);
        viewModel.Cadete = CadeteARecibir;
        var cadeteConPedido = cadeteria.ListadoCadetes.Find(cadete => cadete.listadoPedidos.Any(pedido => pedido.Nro == id));
        cadeteConPedido.listadoPedidos.Remove(viewModel.Pedido);
        return Redirect("ListadoPedidos");
    }

    public IActionResult ListadoPedidos()
    {
        return View(listaViewModel);
    }

    public IActionResult AltaPedido()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

