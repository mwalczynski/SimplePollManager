namespace SimplePollManager.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SimplePollManager.Api.Requests;
    using SimplePollManager.Application.Common.Interfaces;
    using SimplePollManager.Application.Polls.Commands.Close;
    using SimplePollManager.Application.Polls.Commands.Create;
    using SimplePollManager.Application.Polls.Commands.Vote;
    using SimplePollManager.Application.Polls.Dtos;
    using SimplePollManager.Application.Polls.Queries.CheckIfAlreadyVoted;
    using SimplePollManager.Application.Polls.Queries.CheckIfIsOpen;
    using SimplePollManager.Application.Polls.Queries.GetPoll;
    using SimplePollManager.Application.Polls.Queries.GetVotes;

    [ApiController]
    [Route("api/[controller]")]
    public class PollController : ControllerBase
    {
        private readonly ISender mediator;

        private readonly ICurrentUserService currentUserService;

        public PollController(ISender mediator, ICurrentUserService currentUserService)
        {
            this.mediator = mediator;
            this.currentUserService = currentUserService;
        }

        [HttpGet("{pollId}")]
        public async Task<ActionResult<PollDto>> GetPoll(Guid pollId)
        {
            var query = new GetPollQuery()
            {
                PollId = pollId,
            };

            var response = await this.mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{pollId}/votes")]
        public async Task<ActionResult<PollVotesDto>> GetVotes(Guid pollId)
        {
            var query = new GetVotesQuery()
            {
                PollId = pollId,
            };

            var response = await this.mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreatedPollDto>> Create([FromBody] CreatePollRequest request)
        {
            var query = new CreatePollCommand()
            {
                Title = request.Title,
                Question = request.Question,
                CreatorKey = request.CreatorKey,
                PollType = request.PollType,
                Answers = request.Answers
            };

            var response = await this.mediator.Send(query);

            return CreatedAtAction(nameof(GetPoll), new { pollId = response.Id }, response);
        }

        [HttpPost("{pollId}/vote")]
        public async Task<ActionResult<PollDto>> Vote(Guid pollId, [FromBody] VoteRequest request)
        {
            var user = this.currentUserService.User;
            var query = new VoteCommand()
            {
                PollId = pollId,
                User = user,
                AnswerIds = request.Answers,
            };

            var response = await this.mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{pollId}/is-open")]
        public async Task<ActionResult<bool>> CheckIfPollIsOpen(Guid pollId)
        {
            var query = new CheckIfIsOpenQuery()
            {
                PollId = pollId,
            };

            var response = await this.mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{pollId}/already-voted")]
        public async Task<ActionResult<bool>> CheckIfAlreadyVoted(Guid pollId)
        {
            var user = this.currentUserService.User;
            var query = new CheckIfUserAlreadyVotedQuery()
            {
                PollId = pollId,
                User = user,
            };

            var response = await this.mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        [Route("close")]
        public async Task<ActionResult<bool>> Close([FromQuery] Guid pollId, [FromQuery] string creatorKey)
        {
            var user = this.currentUserService.User;
            var query = new ClosePollCommand()
            {
                PollId = pollId,
                User = user,
                CreatorKey = creatorKey,
            };

            var response = await this.mediator.Send(query);
            return Ok(response);
        }
    }
}
