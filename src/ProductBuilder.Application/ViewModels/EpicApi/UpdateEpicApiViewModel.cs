namespace ProductBuilder.Application.ViewModels.EpicApi
{
    using System;

    public class UpdateEpicApiViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid ProductId { get; set; }
    }
}