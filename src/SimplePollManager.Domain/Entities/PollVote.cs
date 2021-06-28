namespace SimplePollManager.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SimplePollManager.Domain.Common;

    public class PollVote : AuditableEntity
    {
        public Guid Id { get; set; }

        public Guid PollId { get; set; }

        public Poll Poll { get; set; }

        public ICollection<PollAnswerVote> PollAnswerVotes { get; private set; } = new List<PollAnswerVote>();

        public static PollVote Create(IList<Guid> answerIds)
        {
            var pollAnswerVotes = answerIds.Select(answerId => new PollAnswerVote()
            {
                PollAnswerId = answerId
            }).ToList();

            return new PollVote()
            {
                PollAnswerVotes = pollAnswerVotes
            };
        }
}
}