using JWTHelper.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace JWTHelper
{
    public class TokkenManager : ITokkenManager
    {
        byte[] Key { get; set; }
        public TokkenManager(byte[] key)
        {
            Key = key;
        }

        //Código de ejemplo
        public string Create(CustomTokken customTokken, double validMinutes = 30)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, customTokken.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, customTokken.Name));


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                // Nuestro token va a durar X tiempo
                Expires = DateTime.UtcNow.AddMinutes(validMinutes),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }

        //Ejemplo
        public CustomTokken GetTokken(HttpContext context)
        {
            CustomTokken customTokken = null;
            if (context.User.Identity is ClaimsIdentity identity)
            {
                var id = identity.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var name = identity.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

                if (id != null)
                {
                    customTokken = new CustomTokken
                    {
                        Id = id,
                        Name = name
                    };
                }
            }

            return customTokken;
        }
    }
}
