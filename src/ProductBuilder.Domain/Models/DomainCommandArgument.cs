namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class DomainCommandArgument : AsdEntity
    {
        public Guid? DomainAggregatePropertyId { get; set; }

        public virtual AggregateProperty AggregateProperty { get; set; }

        public Guid? DomainCommandId { get; set; }

        public virtual Command Command { get; set; }

        public DomainCommandArgument(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected DomainCommandArgument() { }
    }
}