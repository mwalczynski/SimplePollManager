namespace SimplePollManager.Application.Polls.Queries.GetPoll
{
    using System;
    using MediatR;
    using SimplePollManager.Application.Polls.Dtos;

    public class GetPollQuery : IRequest<PollDto>
    {
        public Guid PollId { get; set; }
    }
}
