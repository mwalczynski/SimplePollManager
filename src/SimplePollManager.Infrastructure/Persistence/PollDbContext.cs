namespace SimplePollManager.Infrastructure.Persistence
{
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using SimplePollManager.Application.Common.Interfaces;
    using SimplePollManager.Domain;
    using SimplePollManager.Domain.Common;
    using SimplePollManager.Domain.Entities;

    public class PollDbContext : DbContext, IPollDbContext
    {
        private readonly IDateTime dateTime;

        private readonly ICurrentUserService currentUserService;

        public PollDbContext(
            DbContextOptions<PollDbContext> options,
            IDateTime dateTime,
            ICurrentUserService currentUserService)
            : base(options)
        {
            this.dateTime = dateTime;
            this.currentUserService = currentUserService;

            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Poll> Polls { get; set; }

        public DbSet<PollAnswer> PollAnswers { get; set; }

        public DbSet<PollVote> PollVotes { get; set; }

        public DbSet<PollAnswerVote> PollAnswerVotes { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = this.currentUserService.User;
                        entry.Entity.Created = this.dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
