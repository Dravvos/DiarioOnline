using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BCrypt.Net.BCrypt;

namespace DiarioOnline.Common
{
    public class EncriptionHelper
    {
        public static string Hash(string password, out string salto)
        {
            var random = new Random();
            var rndSaltSize = random.Next(4, 32);
            string salt = GenerateSalt(rndSaltSize);
            salto = salt;
            string hashedPassword = HashPassword(password, salt);
            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return Verify(password, hashedPassword);
        }
    
    }
}
