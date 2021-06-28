namespace SimplePollManager.Application.Polls.Commands.Create
{
    using FluentValidation;

    public class CreatePollValidator : AbstractValidator<CreatePollCommand>
    {
        public CreatePollValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required");

            RuleFor(x => x.Question)
                .NotNull()
                .NotEmpty()
                .WithMessage("Question is required");

            RuleFor(x => x.CreatorKey)
                .NotNull()
                .NotEmpty()
                .WithMessage("CreatorKey is required");

            RuleFor(x => x.Answers)
                .NotNull()
                .Must(x => x.Count > 0)
                .WithMessage("Answers cannot be null or empty");

            RuleForEach(x => x.Answers)
                .NotNull()
                .NotEmpty()
                .WithMessage("Answer cannot be null or empty");
        }
    }
}
