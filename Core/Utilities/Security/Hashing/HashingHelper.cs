using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        // Passwordun Hashi oluşturuluyor.
        public static void CreatePasswordHash
            (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // Bizim için bir class oluşturduğu için () kullandık
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // Her kullanıcı için farklı oluşturuyor
                passwordSalt = hmac.Key;
                // Bir stringin byte karşılığını Encoding.UTF8.GetBytes ile aldık
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        // Burdaki password kullanıcının sonradan girdiği parola
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
