namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class UserRole : AsdEntity
    {
        public string Role { get; set; }

        public Guid? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<UserStory> UserStories { get; set; }

        public UserRole(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected UserRole()
        {
            UserStories = new HashSet<UserStory>();
        }
    }
}