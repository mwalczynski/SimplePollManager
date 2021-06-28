namespace SimplePollManager.Api.IntegrationTests.Infrastructure.DataFeeders
{
    using SimplePollManager.Database;

    public class PollContextDataFeeder
    {
        public static void Feed(PollContext dbContext)
        {
            // Feed here

            dbContext.SaveChanges();
        }
    }
}
