using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        // Girilen kullanıcı bilgisi doğru ise kullanıcının claimlerini içeren Token oluşturacak
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
