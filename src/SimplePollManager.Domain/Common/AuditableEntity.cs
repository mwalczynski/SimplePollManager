using System;

namespace SimplePollManager.Domain.Common
{
    public class AuditableEntity
    {
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }
    }
}
