namespace SimplePollManager.Application.Polls.Queries.GetVotes
{
    using FluentValidation;

    public class GetVotesQueryValidator : AbstractValidator<GetVotesQuery>
    {
        public GetVotesQueryValidator()
        {
            RuleFor(x => x.PollId)
                .NotEmpty()
                .WithMessage("PollId is required");
        }
    }
}
