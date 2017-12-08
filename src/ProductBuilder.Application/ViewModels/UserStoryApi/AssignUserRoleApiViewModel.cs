namespace ProductBuilder.Application.ViewModels.UserStoryApi
{
    using System;

    public class AssignUserRoleApiViewModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid UserRoleId { get; set; }
    }
}