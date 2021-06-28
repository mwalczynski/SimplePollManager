using System;

namespace SimplePollManager.Application.Polls.Commands.Close
{
    using MediatR;

    public class ClosePollCommand : IRequest<Guid>
    {
        public Guid PollId { get; set; }

        public string User { get; set; }

        public string CreatorKey { get; set; }
    }
}
