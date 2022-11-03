using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class CustomTokenHandler
    {
        IConfiguration _config;

        public CustomTokenHandler(IConfiguration config)
        {
            _config = config;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new Token();
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_config["Token:SecurityKey"])
            );
            SigningCredentials _signingCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );
            tokenModel.Expiration = DateTime.Now.AddMinutes(15);

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: _config["Token:Issuer"],
                audience: _config["Token:Audience"],
                expires: tokenModel.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: _signingCredentials
            );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            tokenModel.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtToken);
            tokenModel.RefreshToken = CreateRefreshToken();

            return tokenModel;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
