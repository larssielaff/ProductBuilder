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

        public Aggregate(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected Aggregate() { }
    }
}