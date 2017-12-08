namespace ProductBuilder.Application.ViewModels.TopicApi
{
    using System;

    public class UpdateTopicApiViewModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }
    }
}