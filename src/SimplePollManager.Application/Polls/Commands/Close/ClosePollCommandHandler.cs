namespace SimplePollManager.Application.Polls.Commands.Close
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using SimplePollManager.Application.Common.Exceptions;
    using SimplePollManager.Domain;
    using SimplePollManager.Domain.Entities;

    public class ClosePollCommandHandler : IRequestHandler<ClosePollCommand, Guid>
    {
        private readonly IPollDbContext context;

        public ClosePollCommandHandler(IPollDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> Handle(ClosePollCommand request, CancellationToken cancellationToken)
        {
            var poll = await this.context.Polls.FindAsync(request.PollId);
            if (poll is null)
            {
                throw new NotFoundException(nameof(Poll), request.PollId);
            }

            if (poll.IsCreatorValid(request.User) == false)
            {
                throw new BusinessRuleValidationException("You cannot close poll you have not created");
            }

            if (poll.IsCreatorKeyValid(request.CreatorKey) == false)
            {
                throw new BusinessRuleValidationException("The provider Creator Key is invalid");
            }

            if (poll.IsClosed())
            {
                throw new BusinessRuleValidationException("You cannot close already closed polls");
            }

            poll.Close(request.User, request.CreatorKey);

            await this.context.SaveChangesAsync(cancellationToken);

            return poll.Id;
        }
    }
}
