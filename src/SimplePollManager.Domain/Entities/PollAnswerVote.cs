namespace SimplePollManager.Domain.Entities
{
    using System;

    public class PollAnswerVote
    {
        public Guid Id { get; set; }

        public Guid PollVoteId { get; set; }

        public PollVote PollVote { get; set; }

        public Guid PollAnswerId { get; set; }

        public PollAnswer PollAnswer { get; set; }
    }
}
