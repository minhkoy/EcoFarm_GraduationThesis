using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TokenHandler.Interfaces;
using TokenHandler.Models;

namespace TokenHandler.Services
{
    public class AuthService : IAuthService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly JwtOptionConfig _option;
        public JwtSecurityToken Token { get; set; } = new JwtSecurityToken();
        public AuthService(IHttpContextAccessor httpContextAccessor,
            IOptions<JwtOptionConfig> option)
        {
            _httpContextAccessor = httpContextAccessor;
            _option = option.Value;
            Token = GetToken();
        }

        private JwtSecurityToken GetToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_option.Key);

            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", string.Empty);
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _option.Issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return jwtToken;
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Có lỗi khi validate token");
            }

        }
        public List<Claim> GetClaims()
        {
            var claimsIdentity = new ClaimsIdentity(Token.Claims);
            return Token.Claims.ToList();
        }

        public void TestUserIdentity()
        {

        }
        public DateTime GetExpireDateTime()
        {
            return default;
            //return _token.Claims.FirstOrDefault(x => x.);
        }

        public string GetFullname()
        {
            if (Token is null || Token.Claims is null || Token.Claims.Count() == 0)
            {
                return "System";
            }
            return Token.Claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value;
        }

        public string GetListRole()
        {
            throw new NotImplementedException();
        }

        public string GetUsername()
        {
            if (Token is null || Token.Claims is null || Token.Claims.Count() == 0)
            {
                return "system";
            }
            return Token.Claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
        }

        public string GetAccountEntityId()
        {
            if (Token is null || Token.Claims is null || Token.Claims.Count() == 0)
            {
                return string.Empty;
            }
            return Token.Claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid)).Value;
        }

        public string GenerateToken(AccountTokenData data)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, data.Fullname),
                //new Claim(ClaimTypes.Email, data.Email),
                new Claim(ClaimTypes.Sid, data.EntityId),
                new Claim(ClaimTypes.NameIdentifier, data.Username),
                //new Claim(ClaimTypes.Expiration, data.ExpireDateTime.ToString()),
            }) ;
            //data.Roles.ForEach(x => identity.Claims.Append(new Claim(ClaimTypes.Role, x)));

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_option.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); 
            var principal = new ClaimsPrincipal(identity);
            var token = new JwtSecurityToken(
                issuer: _option.Issuer,
                audience: _option.Audience,
                claims: identity.Claims,
                expires: DateTime.Now.AddMonths(3),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
