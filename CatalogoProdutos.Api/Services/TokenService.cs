using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace CatalogoProdutos.Api.Services
{
    public class TokenService : ITokenService
    {
        public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration config)
        {
            //pega a chave em string no app settings
            var key = config.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new InvalidOperationException("Secret Key Inválida");

            //tranforma a chave em bytes UTF8
            var privateKey = Encoding.UTF8.GetBytes(key);

            //usa chave em bytes para criar as credenciais que vão assinar o token, usando algoritomo de encriptação HmacSha256Signature
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

            //cria o descritor do token (diz como o token vai se comportar quando for gerado)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(config.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
                Audience = config.GetSection("JWT").GetValue<string>("ValidAudience"),
                Issuer = config.GetSection("JWT").GetValue<string>("ValidIssuer"),
                SigningCredentials = signingCredentials

            };

            //intancia o gerador de tokens
            var tokenHandler = new JwtSecurityTokenHandler();

            //cria o token
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return token;
        }

        public string GenerateRefreshToken()
        {
            //cria array de 128 bytes
            var secureRandomBytes = new byte[128];

            //cria um instancia da classe randomnumber generator
            using var randomNumberGenerator = RandomNumberGenerator.Create();

            //preenche array de bytes com numeros aletórios usando numbergenerator
            randomNumberGenerator.GetBytes(secureRandomBytes);

            //converte os bytes aleatórios gerados para uma representação de string
            var refreshToken = Convert.ToBase64String(secureRandomBytes);

            return refreshToken;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration config)
        {
            //obtem secret key
            var secretKey = config["JWT:SecretKey"] ?? throw new InvalidOperationException("Invalid Key");

            //define parametros de validação para o token expirado
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            };

            //instancia token handler para manipular o token
            var tokenHandler = new JwtSecurityTokenHandler();
            //usa o metodo ValidateToken para validar o token jwt com base nos parametros de validação e gera uma securityToken
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            //valida a securityToken
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;
        }
    }
}
