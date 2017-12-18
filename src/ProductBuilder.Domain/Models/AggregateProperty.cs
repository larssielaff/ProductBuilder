namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class AggregateProperty : AsdEntity
    {
        public bool IsAggregateRoot { get; set; }

        public Guid? AsdAggregateId { get; set; }

        public virtual Aggregate Aggregate { get; set; }

        public string Name { get; set; }

        public Guid? LinkedAggregateId { get; set; }

        public virtual Aggregate LinkedAggregate { get; set; }

        public string Type { get; set; }

        public AggregateProperty(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected AggregateProperty() { }
    }
}