namespace SimplePollManager.Application.Polls.Queries.GetPoll
{
    using FluentValidation;

    public class GetPollQueryValidator : AbstractValidator<GetPollQuery>
    {
        public GetPollQueryValidator()
        {
            RuleFor(x => x.PollId)
                .NotEmpty()
                .WithMessage("PollId is required");
        }
    }
}
