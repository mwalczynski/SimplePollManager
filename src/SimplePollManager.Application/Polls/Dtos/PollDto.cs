namespace SimplePollManager.Application.Polls.Dtos
{
    using System;
    using System.Collections.Generic;

    public class PollDto
    {
        public Guid Id { get; set; }

        public string CreatorKey { get; set; }

        public string Title { get; set; }

        public string Question { get; set; }

        public string Type{ get; set; }

        public bool IsOpen { get; set; }

        public IList<PollAnswerDto> Answers { get; set; }
    }
}
