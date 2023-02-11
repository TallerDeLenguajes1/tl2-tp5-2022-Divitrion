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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}