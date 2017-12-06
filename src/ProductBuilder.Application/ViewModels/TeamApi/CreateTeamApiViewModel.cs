namespace ProductBuilder.Application.ViewModels.TeamApi
{
    using System;

    public class CreateTeamApiViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ProductId { get; set; }
    }
}