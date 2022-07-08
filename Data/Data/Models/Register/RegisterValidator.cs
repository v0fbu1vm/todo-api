using FluentValidation;

namespace Todo.Api.Data.Data.Models.Register
{
    /// <summary>
    /// A validator for <see cref="RegisterRequest"/> to see whether correct data has been given.
    /// </summary>
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(options => options.Name)
                .NotEmpty()
                .Length(2, 50)
                .Must(NameMust);

            RuleFor(options => options.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(options => options.Password)
                .NotEmpty()
                .Length(8, 20)
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$");
        }

        private bool NameMust(string name)
        {
            foreach (var item in name)
            {
                if (!char.IsLetter(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}