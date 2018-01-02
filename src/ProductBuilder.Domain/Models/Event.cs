namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;
    public class Event : AsdEntity
    {
        public string EventName { get; set; }
        public Guid? AsdAggregateId { get; set; }
        public virtual Aggregate Aggregate { get; set; }
        public Event(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }
        protected Event() { }
    }
}