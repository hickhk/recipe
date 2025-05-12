using System.Security.Cryptography;
using System.Text;

namespace RecipeBook.Application.Services.Criptografia
{
    public class Encrypt
    {
        private readonly string _additionalKey;
        public Encrypt(string additionalKey) => _additionalKey = additionalKey;
       

        public string EncryptPassword(string password)
        {

            var newPassword = $"{password}{_additionalKey}";

            var bytes = Encoding.UTF8.GetBytes(newPassword);

            var hashBites = SHA512.HashData(bytes);

            return StringBytes(hashBites);
        }

        private static string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
