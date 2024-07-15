using System;
using Doc.Management.ValueObjects;

namespace Doc.Management.CQRS
{
    public abstract class Aggregate
    {
        /// <summary>
        /// Gets or sets the id
        /// warning: do not put the setter to private (used by Marten)
        /// </summary>
        public Guid Id { get; protected set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the version
        /// warning: do not put the setter to private (used by Marten)
        /// </summary>
        public long Version { get; set; }

        protected void SetId(Guid id) => Id = id;

        protected void IncrementVersion() => Version++;
    }
}
