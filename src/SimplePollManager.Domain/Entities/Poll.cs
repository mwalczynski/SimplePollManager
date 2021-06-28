namespace SimplePollManager.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SimplePollManager.Domain.Common;
    using SimplePollManager.Domain.Enums;

    public class Poll : AuditableEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Question { get; set; }

        public string CreatorKey { get; set; }

        public PollType PollType { get; set; }

        public bool IsOpen { get; set; }

        public ICollection<PollAnswer> Answers { get; private set; } = new List<PollAnswer>();

        public ICollection<PollVote> Votes { get; private set; } = new List<PollVote>();

        public static Poll Create(
            string title,
            string question,
            string creatorKey,
            PollType pollType,
            IEnumerable<string> answers,
            IEnumerable<string> existingCreatorKeys)
        {

            if (CanCreate(creatorKey, existingCreatorKeys) == false)
            {
                throw new InvalidOperationException();
            }

            return new Poll()
            {
                Title = title,
                Question = question,
                CreatorKey = creatorKey,
                PollType = pollType,
                IsOpen = true,
                Answers = PollAnswer.Create(answers),
            };
        }

        public bool HasUserAlreadyVoted(string user)
        {
            var userAlreadyVoted = this.Votes.Any(x => x.CreatedBy == user);
            return userAlreadyVoted;
        }

        public bool AreGivenAnswersAvailable(IList<Guid> requestAnswerIds)
        {
            var answerIds = this.Answers.Select(x => x.Id).ToList();
            var unavailableAnswers = requestAnswerIds.Except(answerIds);
            return unavailableAnswers.Any() == false;
        }

        public bool IsNumberOfAnswerVotesCorrect(IList<Guid> requestAnswerIds)
        {
            if (PollType == PollType.MultipleChoice)
            {
                return true;
            }

            var isNumberOfAnswerVotesCorrect = requestAnswerIds.Count == 1;
            return isNumberOfAnswerVotesCorrect;
        }

        public bool CanVote(string user, IList<Guid> answerIds)
        {
            if (this.IsClosed())
            {
                return false;
            }

            if (DuplicatedVotes(answerIds))
            {
                return false;
            }

            if (this.HasUserAlreadyVoted(user))
            {
                return false;
            }

            if (this.AreGivenAnswersAvailable(answerIds) == false)
            {
                return false;
            }

            if (this.IsNumberOfAnswerVotesCorrect(answerIds) == false)
            {
                return false;
            }

            return true;
        }

        public bool IsClosed()
        {
            return this.IsOpen == false;
        }

        public PollVote Vote(string user, IList<Guid> answerIds)
        {
            if (this.CanVote(user, answerIds) == false)
            {
                throw new InvalidOperationException();
            }

            var vote = PollVote.Create(answerIds);

            this.Votes.Add(vote);

            return vote;
        }

        public void Close(string user, string creatorKey)
        {
            if (this.CanClose(user, creatorKey) == false)
            {
                throw new InvalidOperationException();
            }

            this.IsOpen = false;
        }

        private bool CanClose(string user, string creatorKey)
        {
            if (this.IsCreatorValid(user) == false)
            {
                return false;
            }

            if (this.IsCreatorKeyValid(creatorKey) == false)
            {
                return false;
            }

            if (this.IsClosed())
            {
                return false;
            }

            return true;
        }

        public bool IsCreatorKeyValid(string creatorKey)
        {
            return this.CreatorKey == creatorKey;
        }

        public bool IsCreatorValid(string user)
        {
            return this.CreatedBy == user;
        }

        public static bool DuplicatedVotes(IList<Guid> answerIds)
        {
            return answerIds.Count != answerIds.Distinct().Count();
        }

        public static bool CanCreate(string creatorKey, IEnumerable<string> existingCreatorKeys)
        {
            if (CreatorKeyAlreadyExists(creatorKey, existingCreatorKeys))
            {
                return false;
            }

            return true;
        }

        public static bool CreatorKeyAlreadyExists(string creatorKey, IEnumerable<string> existingCreatorKeys)
        {
            var creatorKeyAlreadyExists = existingCreatorKeys.Contains(creatorKey);
            return creatorKeyAlreadyExists;
        }
    }
}
