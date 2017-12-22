namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class Query : AsdEntity
    {
        public Guid? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string QueryName { get; set; }

        public Guid? AsdAggregateId { get; set; }

        public virtual Aggregate Aggregate { get; set; }

        public string RouteTemplate { get; set; }

        public Query(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected Query() { }
    }
}