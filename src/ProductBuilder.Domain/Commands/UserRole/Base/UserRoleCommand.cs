namespace ProductBuilder.Domain.Commands.UserRole.Base
{
    using Asd.Domain.Core.Commands;
    using System;

    public abstract class UserRoleCommand : AsdCommand
    {
        public Guid Id { get; protected set; }

        public string Role { get; protected set; }

        public Guid ProductId { get; protected set; }
    }
}