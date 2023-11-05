using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;

namespace RandomPasscode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetInt32("counter") == null)
        {
            HttpContext.Session.SetInt32("counter", 0);
        }
        return View();
    }

    [HttpPost("generate")]
    public IActionResult Generate()
    {
        int? counter = HttpContext.Session.GetInt32("counter");
        HttpContext.Session.SetInt32("counter", (int)counter + 1);
        string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        int longitud = 14;

        char[] cadenaAleatoria = new char[longitud];

        for (int i = 0; i < longitud; i++)
        {
            cadenaAleatoria[i] = caracteresPermitidos[random.Next(caracteresPermitidos.Length)];
        }

        string cadenaAleatoriaFinal = new string(cadenaAleatoria);
        HttpContext.Session.SetString("Passcode", cadenaAleatoriaFinal);
        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
