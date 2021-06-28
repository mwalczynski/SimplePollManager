namespace SimplePollManager.Database
{
    using Microsoft.EntityFrameworkCore;
    using SimplePollManager.Core;

    public class PollContext : DbContext, IPollContext
    {
        public PollContext(DbContextOptions<PollContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
