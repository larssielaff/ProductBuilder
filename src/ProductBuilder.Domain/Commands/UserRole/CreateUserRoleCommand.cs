﻿namespace ProductBuilder.Domain.Commands.UserRole
{
    using ProductBuilder.Domain.Commands.UserRole.Base;
    using ProductBuilder.Domain.Validations.UserRole;
    using System;

    public class CreateUserRoleCommand : UserRoleCommand
    {
        public CreateUserRoleCommand(Guid id, string role, Guid aggregateId)
        {
            Id = id;
            Role = role;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UserRoleValidator<UserRoleCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}