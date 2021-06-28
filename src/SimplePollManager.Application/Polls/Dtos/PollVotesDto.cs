namespace SimplePollManager.Application.Polls.Dtos
{
    using System.Collections.Generic;

    public class PollVotesDto
    {
        public string Title { get; set; }

        public string Question { get; set; }

        public IList<PollVoteDto> Votes { get; set; }
    }
}
