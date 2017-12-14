namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class AcceptanceCriteria : AsdEntity
    {
        public Guid? UserStoryId { get; set; }

        public virtual UserStory UserStory { get; set; }

        public string Title { get; set; }

        public AcceptanceCriteria(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected AcceptanceCriteria() { }
    }
}