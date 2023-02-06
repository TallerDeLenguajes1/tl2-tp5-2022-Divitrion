using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cadeteria.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authorization;

namespace Cadeteria.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetInt32("Rol") == null)
            {
                return RedirectToAction("Login","Login");
            }
        return View();
    }

    public IActionResult Privacy()
    {
        if (HttpContext.Session.GetInt32("Rol") == null)
            {
               return RedirectToAction("Login","Login");
            }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
