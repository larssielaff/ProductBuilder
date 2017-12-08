namespace ProductBuilder.Application.ViewModels.UserStoryApi
{
    using System;

    public class UpdateStoryPointsApiViewModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int StoryPoints { get; set; }
    }
}