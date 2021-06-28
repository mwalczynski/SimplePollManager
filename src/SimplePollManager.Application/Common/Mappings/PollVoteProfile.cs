namespace SimplePollManager.Application.Common.Mappings
{
    using AutoMapper;
    using SimplePollManager.Application.Polls.Dtos;
    using SimplePollManager.Domain.Entities;

    public class PollVoteProfile : Profile
    {
        public PollVoteProfile()
        {
            CreateMap<PollVote, PollVoteDto>();
        }
    }
}
