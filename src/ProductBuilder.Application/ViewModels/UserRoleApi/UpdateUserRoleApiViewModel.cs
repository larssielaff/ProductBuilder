namespace ProductBuilder.Application.ViewModels.UserRoleApi
{
    using System;

    public class UpdateUserRoleApiViewModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Role { get; set; }
    }
}