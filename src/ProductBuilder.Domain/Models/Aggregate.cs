namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class Aggregate : AsdEntity
    {
        public Guid? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string NamePluralized { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AggregateProperty> AggregateProperties { get; set; }
        
        public virtual ICollection<Query> Queries { get; set; }

        public virtual ICollection<AggregateProperty> LinkedAggregateProperties { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public Aggregate(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected Aggregate()
        {
            AggregateProperties = new HashSet<AggregateProperty>();
            Queries = new HashSet<Query>();
            LinkedAggregateProperties = new HashSet<AggregateProperty>();
            Events = new HashSet<Event>();
        }
    }
}