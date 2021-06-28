namespace SimplePollManager.Application.Polls.Queries.CheckIfAlreadyVoted
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using SimplePollManager.Application.Common.Exceptions;
    using SimplePollManager.Domain;
    using SimplePollManager.Domain.Entities;

    public class CheckIfUserAlreadyVotedQueryHandler : IRequestHandler<CheckIfUserAlreadyVotedQuery, bool>
    {
        private readonly IPollDbContext context;

        public CheckIfUserAlreadyVotedQueryHandler(IPollDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(CheckIfUserAlreadyVotedQuery request, CancellationToken cancellationToken)
        {
            var poll = await this.context.Polls
                .AsNoTracking()
                .Include(p => p.Votes)
                .SingleOrDefaultAsync(p => p.Id == request.PollId, cancellationToken);

            if (poll is null)
            {
                throw new NotFoundException(nameof(Poll), request.PollId);
            }

            var hasAlreadyVoted = poll.HasUserAlreadyVoted(request.User);
            return hasAlreadyVoted;
        }
    }
}