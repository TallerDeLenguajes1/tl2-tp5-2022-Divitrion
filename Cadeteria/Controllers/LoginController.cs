using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cadeteria.Models;
using AutoMapper;
using Cadeteria.ViewModels;
using Cadeteria.Repositorios;
using Microsoft.AspNetCore.Session;

namespace Cadeteria.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepositorioUsuarios _repoUsuarios;
    private readonly IRepositorioCadetes _repoCadetes; 
    private IMapper _mapper;

    public LoginController(ILogger<HomeController> logger,IMapper mapper, IRepositorioUsuarios repoUsuarios, IRepositorioCadetes repoCadetes)
    {
        _logger = logger;
        _mapper = mapper;
        _repoUsuarios = repoUsuarios;
        _repoCadetes = repoCadetes;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Logeo(UsuarioViewmodel UsuarioVM)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var usuario = _repoUsuarios.getUser(UsuarioVM.Usuario,UsuarioVM.Password);
                if (usuario.Id != -1)
                {
                    HttpContext.Session.SetString("Nombre", usuario.Nombre);
                    HttpContext.Session.SetString("Usuario", usuario.Username);
                    HttpContext.Session.SetInt32("Rol", usuario.Rol);
                    if (usuario.Rol == 2)
                    {
                        HttpContext.Session.SetInt32("CadeteID", _repoCadetes.GetUser(usuario.Id).Id);
                    }
                    _logger.LogInformation("El usuario "+usuario.Nombre+" Inicio Sesion");
                    return RedirectToAction("Index","Home");
                }else
                {
                    return View("Login");
                }
            }else
                {
                    return View("Login");
                }
        }
        catch (System.Exception)
        {
            _logger.LogError("Error en el Login de Usuarios");
            return View("UserError");
        }
    }

    public IActionResult LogOut()
    {
        _logger.LogInformation("El usuario "+HttpContext.Session.GetString("Nombre")+" Cerro Sesion");
        HttpContext.Session.Clear();
        return View("Login");
    }

    public IActionResult UserError()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}