namespace ProductBuilder.Application.ViewModels.UserStory
{
    using System;

    public class ProductUserStoryViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Story { get; set; }

        public Guid? UserRoleId { get; set; }

        public Guid? EpicId { get; set; }

        public Guid? TopicId { get; set; }

        public int StoryPoints { get; set; }

        public int Value { get; set; }
    }
}
