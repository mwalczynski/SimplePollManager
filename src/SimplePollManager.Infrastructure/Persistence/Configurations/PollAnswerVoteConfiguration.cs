namespace SimplePollManager.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SimplePollManager.Domain.Entities;

    public class PollAnswerVoteConfiguration : IEntityTypeConfiguration<PollAnswerVote>
    {
        public void Configure(EntityTypeBuilder<PollAnswerVote> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
