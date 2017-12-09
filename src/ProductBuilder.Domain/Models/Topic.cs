namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class Topic : AsdEntity
    {
        public Guid? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public virtual ICollection<UserStory> UserStories { get; set; }

        public Topic(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected Topic()
        {
            UserStories = new HashSet<UserStory>();
        }
    }
}