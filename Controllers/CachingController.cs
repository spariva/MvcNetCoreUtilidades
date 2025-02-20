using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache memoryCache;

        public CachingController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache (Duration = 5, Location = ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida()
        {
            string fecha = DateTime.Now.ToLongDateString() + " -- " + DateTime.Now.ToLongTimeString();
            ViewBag.fecha = fecha;
            return View();
        }

        
        public IActionResult MemoriaPersonalizada(int? tiempo)
        {
            if (tiempo == null) 
            { 
            tiempo = 60;
            }
            string fecha = DateTime.Now.ToLongDateString()
                + " -- "
                + DateTime.Now.ToLongTimeString();
            //DEBEMOS PREGUNTAR SI EXISTE ALGO EN CACHE O NO
            if (this.memoryCache.Get("Fecha") == null)
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));

                this.memoryCache.Set("Fecha", fecha, options);
                ViewData["Mensaje"] = "Fecha almacenada en Cache";
                ViewData["Fecha"] = this.memoryCache.Get("Fecha");
            }
            else
            {
                fecha = this.memoryCache.Get<string>("Fecha");
                ViewData["Mensaje"] = "Fecha recuperada de Cache";
                ViewData["Fecha"] = fecha;
            }
            return View();
        }
    }
}
