using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cadeteria.Models;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;

namespace Cadeteria.Controllers;

public class CadeteriaHubController : Controller
{
    private readonly ILogger<CadeteriaHubController> _logger;
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

