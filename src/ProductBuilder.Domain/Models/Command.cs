namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class Command : AsdEntity
    {
        public string CommandName { get; set; }

        public string RouteTemplate { get; set; }

        public string CommandType { get; set; }

        public Guid? DomainAggregate { get; set; }

        public virtual Aggregate Aggregate { get; set; }

        public Command(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected Command() { }
    }
}