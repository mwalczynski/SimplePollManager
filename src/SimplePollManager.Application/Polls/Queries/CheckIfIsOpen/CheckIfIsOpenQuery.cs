namespace SimplePollManager.Application.Polls.Queries.CheckIfIsOpen
{
    using System;
    using MediatR;

    public class CheckIfIsOpenQuery : IRequest<bool>
    {
        public Guid PollId { get; set; }
    }
}
