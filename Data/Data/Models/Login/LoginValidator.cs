using FluentValidation;

namespace Todo.Api.Data.Data.Models.Login
{
    /// <summary>
    /// A validator for <see cref="LoginRequest"/> to see whether correct data has been given.
    /// </summary>
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(options => options.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(options => options.Password)
                .NotEmpty()
                .Length(8, 20)
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$");
        }
    }
}