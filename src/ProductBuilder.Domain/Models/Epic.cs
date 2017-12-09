namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class Epic : AsdEntity
    {
        public Guid? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserStory> UserStories { get; set; }

        public Epic(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected Epic()
        {
            UserStories = new HashSet<UserStory>();
        }
    }
}