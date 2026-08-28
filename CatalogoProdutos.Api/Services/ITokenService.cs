using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CatalogoProdutos.Api.Services
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration config);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration config);
    }
}
