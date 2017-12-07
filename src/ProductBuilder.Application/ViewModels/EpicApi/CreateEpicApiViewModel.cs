namespace ProductBuilder.Application.ViewModels.EpicApi
{
    using System;

    public class CreateEpicApiViewModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid ProductId { get; set; }

        public string Title { get; set; }
    }
}