namespace ProductBuilder.Application.ViewModels.UserStoryApi
{
    using System;

    public class CreateUserStoryApiViewModel
    {
        public Guid Id { get; set; }

        public string Story { get; set; }

        public Guid ProductId { get; set; }

        public string Title { get; set; }
    }
}