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
    private IMapper _mapper;

    public LoginController(ILogger<HomeController> logger,IMapper mapper, IRepositorioUsuarios repoUsuarios)
    {
        _logger = logger;
        _mapper = mapper;
        _repoUsuarios = repoUsuarios;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Logeo(UsuarioViewmodel UsuarioVM)
    {
        if (ModelState.IsValid)
        {
            var usuario = _repoUsuarios.getUser(UsuarioVM.Usuario,UsuarioVM.Password);
            if (usuario.Id != -1)
            {
                HttpContext.Session.SetString("Nombre", usuario.Nombre);
                HttpContext.Session.SetString("Usuario", usuario.Username);
                HttpContext.Session.SetInt32("Rol", usuario.Rol);
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

    public IActionResult LogOut()
    {
        HttpContext.Session.Clear();
        return View("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}