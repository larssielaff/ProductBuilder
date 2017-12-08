namespace ProductBuilder.Application.ViewModels.UserStoryApi
{
    using System;

    public class UpdateValueApiViewModel
    {
        public Guid Id { get; set; }

        public int Value { get; set; }

        public Guid ProductId { get; set; }
    }
}