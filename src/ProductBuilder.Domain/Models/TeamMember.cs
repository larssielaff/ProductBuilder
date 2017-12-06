namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;
    public class TeamMember : AsdEntity
    {
        public Guid? UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public string Role { get; set; }
        public Guid? TeamId { get; set; }
        public virtual Team Team { get; set; }
        public TeamMember(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }
        protected TeamMember() { }
    }
}