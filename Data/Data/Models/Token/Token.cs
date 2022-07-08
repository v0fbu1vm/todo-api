using System.IdentityModel.Tokens.Jwt;

namespace Todo.Api.Data.Data.Models.Token
{
    /// <summary>
    /// Represents a Token. Where a <see cref="JwtSecurityToken"/> is imbedded into it,
    /// with it's expiration date.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Represents the json web token.
        /// </summary>
        public string Jwt { get; set; } = default!;

        /// <summary>
        /// Represents the expiration date of the imbedded jwt.
        /// </summary>
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow;
    }
}