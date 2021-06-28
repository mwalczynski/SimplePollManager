namespace SimplePollManager.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SimplePollManager.Domain.Entities;

    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(t => t.Title)
                .IsRequired();

            builder
                .Property(t => t.Question)
                .IsRequired();

            builder
                .HasMany(x => x.Answers)
                .WithOne(x => x.Poll)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Votes)
                .WithOne(x => x.Poll)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
