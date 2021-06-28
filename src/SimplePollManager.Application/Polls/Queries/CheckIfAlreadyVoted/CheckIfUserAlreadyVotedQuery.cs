namespace SimplePollManager.Application.Polls.Queries.CheckIfAlreadyVoted
{
    using System;
    using MediatR;

    public class CheckIfUserAlreadyVotedQuery : IRequest<bool>
    {
        public Guid PollId { get; set; }

        public string User { get; set; }
    }
}
