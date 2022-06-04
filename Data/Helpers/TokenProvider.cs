using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todo.Api.Data.Configurations.Settings;
using Todo.Api.Data.Data.Entities;
using Todo.Api.Data.Data.Models.Token;

namespace Todo.Api.Data.Helpers
{
    public class TokenProvider
    {
        #region GenerateToken
        /// <summary>
        /// Generates a json web token for a given <see cref="User"/>.
        /// </summary>
        /// <param name="user">Represents the <see cref="User"/> that needs the token.</param>
        /// <returns>
        /// The generated <see cref="Token"/>. Where a json web token and it's expiration date
        /// is imbedded into it.
        /// </returns>
        public Token GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.SecurityKey);
            DateTime expires = DateTime.UtcNow.AddDays(30);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = AppSettings.Issuer,
                Audience = AppSettings.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new Token()
            {
                Jwt = tokenString,
                ExpirationDate = expires,
            };
        }
        #endregion
    }
}
