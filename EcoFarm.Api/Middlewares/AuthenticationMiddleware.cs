using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TokenHandler.Interfaces;
using TokenHandler.Models;

namespace EcoFarm.Api.Middlewares
{
    public class AuthenticationMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;
        private readonly JwtOptionConfig _option;
        public AuthenticationMiddleware(RequestDelegate next, IAuthService authService,
            IOptions<JwtOptionConfig> options)
        {
            _next = next;
            _authService = authService;
            _option = options.Value;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", string.Empty);
                if (string.IsNullOrEmpty(token))
                {
                    return;
                }
                _authService.Token = GetToken(token);
                await _next(context);
            }
        }

        private JwtSecurityToken GetToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_option.Key);

            //var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", string.Empty);
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;

            return jwtToken;
        }
    }
}
