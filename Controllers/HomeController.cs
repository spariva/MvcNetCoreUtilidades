using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Models;

namespace MvcNetCoreUtilidades.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario)
        {
            HttpContext.Session.SetString("Usuario", usuario);
            ViewBag.Mensaje = "user valido";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Usuario");
            return RedirectToAction("Index");   
        }
    }
}
