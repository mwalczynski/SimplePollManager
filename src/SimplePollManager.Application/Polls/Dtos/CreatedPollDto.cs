namespace SimplePollManager.Application.Polls.Dtos
{
    using System;

    public class CreatedPollDto
    {
        public Guid Id { get; set; }

        public string CreatorKey { set; get; }
    }
}