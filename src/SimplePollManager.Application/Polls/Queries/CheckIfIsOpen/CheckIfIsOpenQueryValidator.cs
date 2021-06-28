namespace SimplePollManager.Application.Polls.Queries.CheckIfIsOpen
{
    using FluentValidation;

    public class CheckIfIsOpenQueryValidator : AbstractValidator<CheckIfIsOpenQuery>
    {
        public CheckIfIsOpenQueryValidator()
        {
            RuleFor(x => x.PollId)
                .NotEmpty()
                .WithMessage("PollId is required");
        }
    }
}
