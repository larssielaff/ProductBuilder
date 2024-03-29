﻿namespace ProductBuilder.Domain.Commands.UserRole
{
    using ProductBuilder.Domain.Commands.UserRole.Base;
    using ProductBuilder.Domain.Validations.UserRole;
    using System;

    public class DeleteUserRoleCommand : UserRoleCommand
    {
        public DeleteUserRoleCommand(Guid id, Guid aggregateId)
        {
            Id = id;
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