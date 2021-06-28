namespace SimplePollManager.Application.Polls.Queries.GetVotes
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using SimplePollManager.Application.Common.Exceptions;
    using SimplePollManager.Application.Polls.Dtos;
    using SimplePollManager.Domain;
    using SimplePollManager.Domain.Entities;

    public class GetVotesQueryHandler : IRequestHandler<GetVotesQuery, PollVotesDto>
    {
        private readonly IPollDbContext context;
        private readonly IMapper mapper;

        public GetVotesQueryHandler(IPollDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PollVotesDto> Handle(GetVotesQuery request, CancellationToken cancellationToken)
        {
            var poll = await this.context.Polls.FindAsync(request.PollId);
            if (poll is null)
            {
                throw new NotFoundException(nameof(Poll), request.PollId);
            }

            var pollVotes = await this.context.PollAnswers
                .Include(pa => pa.Poll)
                .Include(pa => pa.PollAnswerVotes)
                .Where(pa => pa.Poll.Id == request.PollId)
                .Select(pa => new PollVoteDto()
                {
                    Answer = pa.Answer,
                    AnswerId = pa.Id,
                    Count = pa.PollAnswerVotes.Count,
                }).ToListAsync(cancellationToken);

            var result = new PollVotesDto()
            {
                Title = poll.Title,
                Question = poll.Question,
                Votes = pollVotes
            };

            return result;
        }
    }
}
