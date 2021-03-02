using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        // Burası da uydurulmuş securitykeyleri Byte hale getirmeye yarıyor.
        // Daha sonra o byte'ı alıp bir simetrik güvenlik anahtarı haline getiriyor.
        // json web joken'imizin ihtiyaç duyduğu yapılar.
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
