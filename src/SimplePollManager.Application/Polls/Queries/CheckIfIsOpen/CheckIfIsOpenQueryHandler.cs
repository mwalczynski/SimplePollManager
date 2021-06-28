namespace SimplePollManager.Application.Polls.Queries.CheckIfIsOpen
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using SimplePollManager.Application.Common.Exceptions;
    using SimplePollManager.Domain;
    using SimplePollManager.Domain.Entities;

    public class CheckIfIsOpenQueryHandler : IRequestHandler<CheckIfIsOpenQuery, bool>
    {
        private readonly IPollDbContext context;

        public CheckIfIsOpenQueryHandler(IPollDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(CheckIfIsOpenQuery request, CancellationToken cancellationToken)
        {
            var poll = await this.context.Polls.FindAsync(request.PollId);
            if (poll is null)
            {
                throw new NotFoundException(nameof(Poll), request.PollId);
            }

            var result = poll.IsOpen;
            return result;
        }
    }
}