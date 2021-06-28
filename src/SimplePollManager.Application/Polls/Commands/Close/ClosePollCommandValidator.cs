namespace SimplePollManager.Application.Polls.Commands.Close
{
    using FluentValidation;

    public class ClosePollCommandValidator : AbstractValidator<ClosePollCommand>
    {
        public ClosePollCommandValidator()
        {
            RuleFor(x => x.PollId)
                .NotEmpty()
                .WithMessage("PollId is required");

            RuleFor(x => x.User)
                .NotNull()
                .NotEmpty()
                .WithMessage("User is required");

            RuleFor(x => x.CreatorKey)
                .NotNull()
                .NotEmpty()
                .WithMessage("CreatorKey is required");
        }
    }
}
