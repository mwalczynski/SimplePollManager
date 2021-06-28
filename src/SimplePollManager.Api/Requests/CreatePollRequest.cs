namespace SimplePollManager.Api.Requests
{
    using System.Collections.Generic;
    using SimplePollManager.Domain.Enums;

    public class CreatePollRequest
    {
        public string Title { get; set; }

        public string Question { get; set; }

        public string CreatorKey { set; get; }

        public PollType PollType { get; set; }

        public ICollection<string> Answers { get; set; }

    }
}
