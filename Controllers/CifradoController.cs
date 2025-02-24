using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CifradoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CifradoBasico()
        {
            return View();
        }

        //alguno de estos params puede ser null pero al ser string en vez de int no hace falta el ? tipo int?
        [HttpPost]
        public IActionResult CifradoBasico(string contenido, string resultado, string accion)
        {
            string response = HelperCriptography.EncriptarTextoBasico(contenido);
            if (accion.ToLower() == "cifrar")
            {
                ViewBag.TextoCifrado = response;
            }
            else if (accion.ToLower() == "comparar")
            {
                if(response != resultado)
                {
                    ViewBag.Mensaje = "no coincide";
                }
                else
                {
                    ViewBag.Mensaje = "Coincide!";
                }
            }
            return View();
        }

        public IActionResult CifradoEficiente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CifradoEficiente(string contenido, string resultado, string accion)
        {
            if(accion.ToLower() == "cifrar")
            {
                string response = HelperCriptography.CifrarContenido(contenido, false);
                ViewBag.TextoCifrado = response;
                ViewBag.Salt = HelperCriptography.Salt;
            }
            else if(accion.ToLower() == "comparar")
            {
                string response = HelperCriptography.CifrarContenido(contenido, true);
                if (response != resultado)
                {
                    ViewBag.Mensaje = "no coincide";
                }
                else
                {
                    ViewBag.Mensaje = "Coincide!";
                }
            }
            return View();
        }
    }
}
