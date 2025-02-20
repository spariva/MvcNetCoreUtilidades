using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;
using static MvcNetCoreUtilidades.Helpers.HelperPathProvider;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helperPath;

        public UploadFilesController(HelperPathProvider helperPathProvider)
        {
            this.helperPath = helperPathProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            //string rootFolder = this.webHostEnvironment.WebRootPath;
            //string tempFolder = Path.GetTempPath();
            string fileName = fichero.FileName;
            //string path = Path.Combine(rootFolder, "uploads", fileName);
            string path = this.helperPath.MapPath(fileName, Folder.Images);

            using (Stream steam = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(steam);
            }
            ViewBag.Mensaje = "fichero subido a " + path;
            ViewBag.url = this.helperPath.MapUrlPath(fileName, Folder.Images);
            return View();
        }
    }
}
