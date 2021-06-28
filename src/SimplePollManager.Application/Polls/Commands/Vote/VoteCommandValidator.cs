namespace SimplePollManager.Application.Polls.Commands.Vote
{
    using FluentValidation;

    public class VoteCommandValidator : AbstractValidator<VoteCommand>
    {
        public VoteCommandValidator()
        {
            RuleFor(x => x.PollId)
                .NotEmpty()
                .WithMessage("PollId is required");

            RuleFor(x => x.User)
                .NotNull()
                .NotEmpty()
                .WithMessage("User is required");

            RuleFor(x => x.AnswerIds)
                .NotNull()
                .Must(x => x.Count > 0)
                .WithMessage("Votes cannot be null or empty");

            RuleForEach(x => x.AnswerIds)
                .NotNull()
                .NotEmpty()
                .WithMessage("Answer vote cannot be null or empty");
        }
    }
}
