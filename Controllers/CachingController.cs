using Microsoft.AspNetCore.Mvc;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache (Duration = 5, Location =ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida()
        {
            string fecha = DateTime.Now.ToLongDateString() + " -- " + DateTime.Now.ToLongTimeString();
            ViewBag.fecha = fecha;
            return View();
        }
    }
}
