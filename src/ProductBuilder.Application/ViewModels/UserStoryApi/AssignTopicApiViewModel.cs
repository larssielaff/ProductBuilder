namespace ProductBuilder.Application.ViewModels.UserStoryApi
{
    using System;

    public class AssignTopicApiViewModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid TopicId { get; set; }
    }
}