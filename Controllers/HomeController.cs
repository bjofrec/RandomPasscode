using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Text;
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
        int codeNumber =(int)(HttpContext.Session.GetInt32("CodeNumber") ??0);
        string randomCode = HttpContext.Session.GetString("RandomCode");
        if(randomCode == null){
            randomCode = GenerateRandomCode(14);
            HttpContext.Session.SetString("RandomCode", randomCode);
        }
        ViewData["CodeNumber"] = codeNumber + 1;
        return View();
    }
    public IActionResult GenerateNewCode()
    {
        int codeNumber = (HttpContext.Session.GetInt32("CodeNumber") ?? 0) + 1;
        HttpContext.Session.SetInt32("CodeNumber", codeNumber);

        string randomCode = GenerateRandomCode(14);
        HttpContext.Session.SetString("RandomCode", randomCode);

        return RedirectToAction("Index");
    }


    private string GenerateRandomCode(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var code = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            code.Append(characters[random.Next(characters.Length)]);
        }

        return code.ToString();
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
