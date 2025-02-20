using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MvcNetCoreUtilidades.Helpers
{
    public enum Folder { Images, Uploads, Temporal }
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        private IServer server;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IServer server)
        {
            this.hostEnvironment = hostEnvironment;
            this.server = server;
        }

        public string MapPath(string filename, Folder folder)
        {
            string carpeta = this.getFolderName(folder);
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, filename);
            return path;
        }

        public string getFolderName(Folder folder)
        {
            string carpeta = "";
            switch (folder)
            {
                case Folder.Images:
                    carpeta = "images";
                    break;
                case Folder.Uploads:
                    carpeta = "uploads";
                    break;
                case Folder.Temporal:
                    carpeta = "temporal";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(folder), folder, null);
            }
            return carpeta;
        }

        public string MapUrlPath(string filename, Folder folder) 
        {
            string carpeta = this.getFolderName(folder);
            //string url = $"https://localhost:7173/{carpeta}/{filename}";
            var addresses = this.server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = addresses.FirstOrDefault();
            string url = $"{serverUrl}/{carpeta}/{filename}";
            return url;
        }
    }
}
