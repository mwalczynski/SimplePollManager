namespace SimplePollManager.Api.IntegrationTests.Infrastructure.DataFeeders
{
    using SimplePollManager.Infrastructure.Persistence;

    public class PollContextDataFeeder
    {
        public static void Feed(PollDbContext dbDbContext)
        {
            // Feed here

            dbDbContext.SaveChanges();
        }
    }
}
