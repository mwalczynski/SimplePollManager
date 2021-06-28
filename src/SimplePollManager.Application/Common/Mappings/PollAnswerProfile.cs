namespace SimplePollManager.Application.Common.Mappings
{
    using AutoMapper;
    using SimplePollManager.Application.Polls.Dtos;
    using SimplePollManager.Domain.Entities;

    class PollAnswerProfile : Profile
    {
        public PollAnswerProfile()
        {
            CreateMap<PollAnswer, PollAnswerDto>();
        }
    }
}
