namespace SimplePollManager.Application.Polls.Dtos
{
    using System;

    public class PollAnswerDto
    {
        public Guid Id { get; set; }

        public string Answer { get; set; }
    }
}
