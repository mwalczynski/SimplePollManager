namespace SimplePollManager.Api.Requests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class VoteRequest
    {
        [MinLength(1)]
        public IList<Guid> Answers { get; set; }
    }
}
