namespace SimplePollManager.Application.Polls.Queries.GetPoll
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using SimplePollManager.Application.Common.Exceptions;
    using SimplePollManager.Application.Polls.Dtos;
    using SimplePollManager.Domain;
    using SimplePollManager.Domain.Entities;

    public class GetPollQueryHandler : IRequestHandler<GetPollQuery, PollDto>
    {
        private readonly IPollDbContext context;
        private readonly IMapper mapper;

        public GetPollQueryHandler(IPollDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PollDto> Handle(GetPollQuery request, CancellationToken cancellationToken)
        {
            var poll = await this.context.Polls
                .AsNoTracking()
                .Include(p => p.Votes)
                .Where(p => p.Id == request.PollId)
                .ProjectTo<PollDto>(this.mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (poll is null)
            {
                throw new NotFoundException(nameof(Poll), request.PollId);
            }

            return poll;
        }
    }
}