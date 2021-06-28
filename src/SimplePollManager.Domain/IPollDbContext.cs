namespace SimplePollManager.Domain
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SimplePollManager.Domain.Entities;

    public interface IPollDbContext
    {
        DbSet<Poll> Polls { get; set; }

        DbSet<PollAnswer> PollAnswers { get; set; }

        DbSet<PollVote> PollVotes { get; set; }

        DbSet<PollAnswerVote> PollAnswerVotes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
