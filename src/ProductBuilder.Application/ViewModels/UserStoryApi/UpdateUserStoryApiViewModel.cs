namespace ProductBuilder.Application.ViewModels.UserStoryApi
{
    using System;

    public class UpdateUserStoryApiViewModel
    {
        public Guid Id { get; set; }

        public string Story { get; set; }

        public string Title { get; set; }

        public Guid ProductId { get; set; }
    }
}