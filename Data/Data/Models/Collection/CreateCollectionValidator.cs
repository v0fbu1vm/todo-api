using FluentValidation;

namespace Todo.Api.Data.Data.Models.Collection
{
    /// <summary>
    /// A validator for <see cref="CreateCollectionRequest"/> to see whether correct data has been given.
    /// </summary>
    public class CreateCollectionValidator : AbstractValidator<CreateCollectionRequest>
    {
        public CreateCollectionValidator()
        {
            RuleFor(options => options.Name)
                .NotEmpty();
        }
    }
}