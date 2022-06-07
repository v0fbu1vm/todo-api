using FluentValidation;

namespace Todo.Api.Data.Data.Models.Assignment
{
    /// <summary>
    /// A validator for <see cref="UpdateAssignmentRequest"/> to see whether correct data has been given.
    /// </summary>
    public class UpdateAssignmentValidator : AbstractValidator<UpdateAssignmentRequest>
    {
        public UpdateAssignmentValidator()
        {

            RuleFor(options => options.Id)
                .Must(id => Guid.TryParse(id, out _));

            RuleFor(options => options.Title)
                .NotNull()
                .NotEmpty()
                .When(instance => instance.Description == null && instance.DateScheduled == null
                && instance.IsImportant == null && instance.IsCompleted == null);

            RuleFor(options => options.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(240)
                .When(instance => instance.Title == null && instance.DateScheduled == null
                && instance.IsImportant == null && instance.IsCompleted == null);

            RuleFor(options => options.DateScheduled)
                .NotNull()
                .NotEmpty()
                .GreaterThan(DateTime.UtcNow)
                .When(instance => instance.Title == null && instance.Description == null
                && instance.IsImportant == null && instance.IsCompleted == null);

            RuleFor(options => options.IsImportant)
                .NotNull()
                .NotEmpty()
                .When(instance => instance.Title == null && instance.Description == null
                && instance.DateScheduled == null && instance.IsCompleted == null);

            RuleFor(options => options.IsCompleted)
                .NotNull()
                .NotEmpty()
                .When(instance => instance.Title == null && instance.Description == null
                && instance.DateScheduled == null && instance.IsImportant == null);
        }
    }
}
