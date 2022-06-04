namespace Todo.Api.Data.Data.Models.Login
{
    /// <summary>
    /// A model for authenticating a user.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Represents the email of a user.
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// Represents the password of a user.
        /// </summary>
        public string Password { get; set; } = default!;
    }
}
