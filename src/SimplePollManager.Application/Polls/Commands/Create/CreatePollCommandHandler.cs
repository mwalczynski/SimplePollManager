namespace SimplePollManager.Application.Polls.Commands.Create
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using SimplePollManager.Application.Common.Exceptions;
    using SimplePollManager.Application.Polls.Dtos;
    using SimplePollManager.Domain;
    using SimplePollManager.Domain.Entities;

    public class CreatePollCommandHandler : IRequestHandler<CreatePollCommand, CreatedPollDto>
    {
        private readonly IPollDbContext context;

        public CreatePollCommandHandler(IPollDbContext context)
        {
            this.context = context;
        }

        public async Task<CreatedPollDto> Handle(CreatePollCommand request, CancellationToken cancellationToken)
        {
            var existingCreatorKeys = this.context.Polls
                .Select(p => p.CreatorKey)
                .ToList();

            if (Poll.CreatorKeyAlreadyExists(request.CreatorKey, existingCreatorKeys))
            {
                throw new BusinessRuleValidationException("Given Creator Key is already in use");
            }

            var poll = Poll.Create(
                request.Title,
                request.Question,
                request.CreatorKey,
                request.PollType,
                request.Answers,
                existingCreatorKeys);

            await this.context.Polls.AddAsync(poll, cancellationToken);

            await this.context.SaveChangesAsync(cancellationToken);

            var result = new CreatedPollDto()
            {
                Id = poll.Id,
                CreatorKey = poll.CreatorKey,
            };

            return result;
        }
    }
}
