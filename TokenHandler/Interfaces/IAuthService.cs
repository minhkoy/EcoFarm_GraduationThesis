using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Models;

namespace TokenHandler.Interfaces
{
    public interface IAuthService
    {
        JwtSecurityToken Token { get; set; }
        //Get user info from JWT Token
        List<Claim> GetClaims();
        string GetAccountEntityId();
        string GetUsername();
        string GetFullname();
        string GetListRole();
        string GetAccountTypeName();
        DateTime GetExpireDateTime();
        string GenerateToken(AccountTokenData data, bool? isRemember);
    }
}
