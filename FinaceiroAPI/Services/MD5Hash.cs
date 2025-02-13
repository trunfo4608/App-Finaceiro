using System.Security.Cryptography;
using System.Text;

namespace FinaceiroAPI.Services
{
    public class MD5Hash
    {

        public static string GeraHash(string valor)
        {
			try
			{
				MD5 md5 = MD5.Create();

				var bytes = Encoding.ASCII.GetBytes(valor);

				var hash = md5.ComputeHash(bytes);

				StringBuilder sb =new StringBuilder();

				foreach (var item in hash)
				{
					sb.Append(item.ToString("x2"));
				}

				return sb.ToString();

			}
			catch (Exception e)
			{

				throw new Exception($"Erro {e.Message}");
			}
        }
    }
}
