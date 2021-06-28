namespace SimplePollManager.Application.Polls.Queries.CheckIfAlreadyVoted
{
    using FluentValidation;

    public class CheckIfUserAlreadyVotedQueryValidator : AbstractValidator<CheckIfUserAlreadyVotedQuery>
    {
        public CheckIfUserAlreadyVotedQueryValidator()
        {
            RuleFor(x => x.PollId)
                .NotEmpty()
                .WithMessage("PollId is required");

            RuleFor(x => x.User)
                .NotEmpty()
                .WithMessage("User is required");
        }
    }
}
