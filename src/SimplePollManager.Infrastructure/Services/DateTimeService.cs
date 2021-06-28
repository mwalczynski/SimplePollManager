namespace SimplePollManager.Infrastructure.Services
{
    using System;
    using SimplePollManager.Application.Common.Interfaces;

    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
