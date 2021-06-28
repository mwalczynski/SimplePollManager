namespace SimplePollManager.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PollAnswer
    {
        public Guid Id { get; set; }

        public Guid PollId { get; set; }

        public Poll Poll { get; set; }

        public string Answer { get; set; }

        public ICollection<PollAnswerVote> PollAnswerVotes { get; private set; } = new List<PollAnswerVote>();

        public static PollAnswer Create(string answer)
        {
            return new PollAnswer()
            {
                Answer = answer
            };
        }

        public static ICollection<PollAnswer> Create(IEnumerable<string> answers)
        {
            return answers.Select(a => Create(a)).ToList();
        }
    }
}