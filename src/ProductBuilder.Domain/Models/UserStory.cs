namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class UserStory : AsdEntity
    {
        public Guid? UserRoleId { get; set; }

        public virtual UserRole UserRole { get; set; }

        public Guid? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string Story { get; set; }

        public Guid? TopicId { get; set; }

        public virtual Topic Topic { get; set; }

        public Guid? EpicId { get; set; }

        public virtual Epic Epic { get; set; }

        public int Value { get; set; }

        public int StoryPoints { get; set; }

        public string Title { get; set; }

        public virtual ICollection<AcceptanceCriteria> AcceptanceCriterias { get; set; }

        public UserStory(Guid id) : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected UserStory()
        {
            AcceptanceCriterias = new HashSet<AcceptanceCriteria>();
        }
    }
}