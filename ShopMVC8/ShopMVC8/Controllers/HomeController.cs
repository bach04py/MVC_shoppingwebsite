using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShopMVC8.Data;
using ShopMVC8.Models;

namespace ShopMVC8.Controllers
{

public class HomeController  : Controller
{
    private readonly Hshop2023Context db;
    public HomeController(Hshop2023Context context)
        {
            db = context;
        }

    public IActionResult Index()
    {
        var data = db.HangHoas;
        return View("Index",data);
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
}