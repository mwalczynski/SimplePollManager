namespace SimplePollManager.Application.Polls.Queries.GetVotes
{
    using System;
    using MediatR;
    using SimplePollManager.Application.Polls.Dtos;

    public class GetVotesQuery : IRequest<PollVotesDto>
    {
        public Guid PollId { get; set; }
    }
}
