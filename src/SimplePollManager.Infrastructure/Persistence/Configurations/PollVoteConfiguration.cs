namespace SimplePollManager.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SimplePollManager.Domain.Entities;

    public class PollVoteConfiguration : IEntityTypeConfiguration<PollVote>
    {
        public void Configure(EntityTypeBuilder<PollVote> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.PollAnswerVotes)
                .WithOne(x => x.PollVote)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
