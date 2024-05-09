using Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Application.Users.Services
{
    public class CryptoService : ICryptoService
    {
        public bool Equals(string md5, string input)
            => string.Equals(md5, GetMD5Hash(input), StringComparison.InvariantCultureIgnoreCase);

        public string GetMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
