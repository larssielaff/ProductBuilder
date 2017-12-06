namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;
    public class Team : AsdEntity
    {
        public string Title { get; set; }
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public Team(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }
        protected Team()
        {
            TeamMembers = new HashSet<TeamMember>();
        }
    }
}