namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;
    public class UserProfile : AsdEntity
    {
        public string EmailAddress { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public UserProfile(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }
        protected UserProfile()
        {
            TeamMembers = new HashSet<TeamMember>();
        }
    }
}