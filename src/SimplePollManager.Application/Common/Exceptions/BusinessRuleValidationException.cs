namespace SimplePollManager.Application.Common.Exceptions
{
    using System;

    public class BusinessRuleValidationException : Exception
    {
        public string Details { get; }

        public BusinessRuleValidationException(string message)
            : base(message)
        {
            this.Details = message;
        }
    }
}
