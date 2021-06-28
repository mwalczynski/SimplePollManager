namespace SimplePollManager.Application.Polls.Commands.Create
{
    using System.Collections.Generic;
    using MediatR;
    using SimplePollManager.Application.Polls.Dtos;
    using SimplePollManager.Domain.Enums;

    public class CreatePollCommand : IRequest<CreatedPollDto>
    {
        public string Title { get; set; }

        public string Question { get; set; }

        public string CreatorKey { set; get; }

        public PollType PollType { get; set; }

        public ICollection<string> Answers { get; set; }
    }
}
