namespace SimplePollManager.Application.Polls.Commands.Vote
{
    using System;
    using System.Collections.Generic;
    using MediatR;

    public class VoteCommand : IRequest<Guid>
    {
        public Guid PollId { get; set; }

        public string User { get; set; }

        public IList<Guid> AnswerIds { get; set; }
    }
}
