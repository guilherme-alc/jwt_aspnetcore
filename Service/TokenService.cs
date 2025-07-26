using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtAspnet.Service
{
    public class TokenService
    {
        public string Generate()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.PrivateKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var handler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(6),
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}
