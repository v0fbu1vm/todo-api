using FluentValidation;

namespace Todo.Api.Data.Data.Models.Assignment
{
    /// <summary>
    /// A validator for <see cref="CreateAssignmentRequest"/> to see whether correct data has been given.
    /// </summary>
    public class CreateAssignmentValidator : AbstractValidator<CreateAssignmentRequest>
    {
        public CreateAssignmentValidator()
        {
            RuleFor(options => options.Title)
                .NotEmpty();

            RuleFor(options => options.Description)
                .MaximumLength(240);

            RuleFor(options => options.DateScheduled)
                .GreaterThan(DateTime.UtcNow);

            RuleFor(options => options.CollectionId)
                .Must(id => Guid.TryParse(id, out _));
        }
    }
}