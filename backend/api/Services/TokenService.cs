using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.models;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration _config;
        private SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SignInKey"]));
        }

        public string CreateToken(AppUser user)
        {

            var claims =new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var descriptor = new SecurityTokenDescriptor
            {
                Audience = _config["JWT:Audience"],
                Issuer = _config["JWT:Issuer"],
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = creds,
                Subject = new ClaimsIdentity(claims)
            };

            var TokenHandler =  new JwtSecurityTokenHandler();

            var Token = TokenHandler.CreateToken(descriptor);

            return TokenHandler.WriteToken(Token);
        }
    }
}