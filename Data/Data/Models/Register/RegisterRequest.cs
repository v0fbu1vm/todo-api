namespace Todo.Api.Data.Data.Models.Register
{
    /// <summary>
    /// A model for registring a user.
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// Represents the name of the user.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Represents the email of the user.
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// Represents the password of the user.
        /// </summary>
        public string Password { get; set; } = default!;
    }
}