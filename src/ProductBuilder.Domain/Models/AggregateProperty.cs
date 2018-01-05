namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class AggregateProperty : AsdEntity
    {
        public string LinkedAggregateName { get; set; }

        public bool IsAggregateRoot { get; set; }

        public Guid? AsdAggregateId { get; set; }

        public virtual Aggregate Aggregate { get; set; }

        public string Name { get; set; }

        public Guid? LinkedAggregateId { get; set; }

        public virtual Aggregate LinkedAggregate { get; set; }

        public string Type { get; set; }

        public virtual ICollection<DomainCommandArgument> DomainCommandArguments { get; set; }

        public AggregateProperty(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected AggregateProperty()
        {
            DomainCommandArguments = new HashSet<DomainCommandArgument>();
        }
    }
}