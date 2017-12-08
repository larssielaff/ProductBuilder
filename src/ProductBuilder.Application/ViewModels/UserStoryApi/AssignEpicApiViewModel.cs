namespace ProductBuilder.Application.ViewModels.UserStoryApi
{
    using System;

    public class AssignEpicApiViewModel
    {
        public Guid Id { get; set; }

        public Guid EpicId { get; set; }

        public Guid ProductId { get; set; }
    }
}