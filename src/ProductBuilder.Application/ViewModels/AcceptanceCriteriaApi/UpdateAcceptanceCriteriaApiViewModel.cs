namespace ProductBuilder.Application.ViewModels.AcceptanceCriteriaApi
{
    using System;

    public class UpdateAcceptanceCriteriaApiViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid UserStoryId { get; set; }
    }
}