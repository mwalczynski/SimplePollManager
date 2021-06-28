namespace SimplePollManager.Application.Polls.Dtos
{
    using System;

    public class PollVoteDto
    {
        public Guid AnswerId { get; set; }

        public string Answer { get; set; }

        public int Count{ get; set; }
    }
}
