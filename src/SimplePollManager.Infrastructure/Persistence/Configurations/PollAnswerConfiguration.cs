namespace SimplePollManager.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SimplePollManager.Domain.Entities;

    public class PollAnswerConfiguration : IEntityTypeConfiguration<PollAnswer>
    {
        public void Configure(EntityTypeBuilder<PollAnswer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Answer)
                .IsRequired();

            builder.HasMany(x => x.PollAnswerVotes)
                .WithOne(x => x.PollAnswer)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
