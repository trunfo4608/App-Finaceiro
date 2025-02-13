using FinaceiroAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace FinaceiroAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GeraToken(Usuario usuario)
        {
            var tokenHandle = new JwtSecurityTokenHandler();

            var chave = Encoding.ASCII.GetBytes(_configuration.GetSection("Chave").Get<string>());

            var tokenDescritor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, usuario.Login.ToString()),
                            new Claim(ClaimTypes.Role, usuario.Funcao.ToString()),
                        }
                    ),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature
                        )
            };

            var token = tokenHandle.CreateToken(tokenDescritor);

            return tokenHandle.WriteToken(token);

        }
    }
}
