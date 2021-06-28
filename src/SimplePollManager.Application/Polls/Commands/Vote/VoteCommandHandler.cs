namespace SimplePollManager.Application.Polls.Commands.Vote
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using SimplePollManager.Application.Common.Exceptions;
    using SimplePollManager.Domain;
    using SimplePollManager.Domain.Entities;

    public class VoteCommandHandler : IRequestHandler<VoteCommand, Guid>
    {
        private readonly IPollDbContext context;

        public VoteCommandHandler(IPollDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> Handle(VoteCommand request, CancellationToken cancellationToken)
        {
            var poll = await this.context.Polls
                .Include(p => p.Answers)
                .Include(p => p.Votes)
                .ThenInclude(v => v.PollAnswerVotes)
                .SingleOrDefaultAsync(p => p.Id == request.PollId, cancellationToken);

            if (poll is null)
            {
                throw new NotFoundException(nameof(Poll), request.PollId);
            }

            if (poll.IsClosed())
            {
                throw new BusinessRuleValidationException("You cannot vote in a closed poll");
            }

            if (Poll.DuplicatedVotes(request.AnswerIds))
            {
                throw new BusinessRuleValidationException("Votes cannot be duplicated");
            }

            if (poll.HasUserAlreadyVoted(request.User))
            {
                throw new BusinessRuleValidationException("You have already voted in this poll");
            }

            if (poll.AreGivenAnswersAvailable(request.AnswerIds) == false)
            {
                throw new BusinessRuleValidationException("You provided incorrect answers");
            }

            if (poll.IsNumberOfAnswerVotesCorrect(request.AnswerIds) == false)
            {
                throw new BusinessRuleValidationException("You provided incorrect answers");
            }

            var vote = poll.Vote(request.User, request.AnswerIds);

            await this.context.SaveChangesAsync(cancellationToken);

            return vote.Id;
        }
    }
}
