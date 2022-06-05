using FluentValidation;

namespace Todo.Api.Data.Data.Models.Collection
{
    /// <summary>
    /// A validator for <see cref="UpdateCollectionRequest"/> to see whether correct data has been given.
    /// </summary>
    public class UpdateCollectionValidator : AbstractValidator<UpdateCollectionRequest>
    {
        public UpdateCollectionValidator()
        {
            RuleFor(options => options.Id)
                .NotEmpty()
                .Must(id => Guid.TryParse(id, out _));

            RuleFor(options => options.Name)
                .NotEmpty();
        }
    }
}
