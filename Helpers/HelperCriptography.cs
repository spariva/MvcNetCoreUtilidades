using System.Security.Cryptography;
using System.Text;

namespace MvcNetCoreUtilidades.Helpers
{
    public class HelperCriptography()
    {
        public static string Salt {  get; set; } //no se necesita era solo para dibujarla

        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for(int i = 0; i <= 30; i++)
            {
                int aleat = random.Next(1, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        public static string CifrarContenido(string contenido, bool comparar)
        {
            if (!comparar) { 
            Salt = GenerateSalt();
            }

            string contenidoSalt = contenido + Salt;
            SHA256 managed = SHA256.Create();
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            salida = encoding.GetBytes(contenidoSalt);

            for (int i = 0; i < 5; i++)
            { 
                salida = managed.ComputeHash(salida);
            }
            //liberar la memoria!
            managed.Clear();
            //efecto visual:
            string resultado = encoding.GetString(salida);
            return resultado;
        }

        public static string EncriptarTextoBasico(string texto) 
        {
            byte[] entrada;
            byte[] salida;
            //encoding cambia de string a byte
            UnicodeEncoding encoding = new UnicodeEncoding();
            //sha1 cifra
            SHA1 managed = SHA1.Create();

            entrada = encoding.GetBytes(texto);
            salida = managed.ComputeHash(entrada);
            //efecto visual
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}
