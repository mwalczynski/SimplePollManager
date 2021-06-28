namespace SimplePollManager.Application.Common.Mappings
{
    using AutoMapper;
    using SimplePollManager.Application.Polls.Dtos;
    using SimplePollManager.Domain.Entities;

    public class PollProfile : Profile
    {
        public PollProfile()
        {
            CreateMap<Poll, PollDto>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.PollType.ToString()));
        }
    }
}
