﻿namespace ProductBuilder.Domain.Commands.UserStory
{
    using ProductBuilder.Domain.Commands.UserStory.Base;
    using ProductBuilder.Domain.Validations.UserStory;
    using System;

    public class AssignUserRoleCommand : UserStoryCommand
    {
        public AssignUserRoleCommand(Guid id, Guid userRoleId, Guid aggregateId)
        {
            Id = id;
            UserRoleId = userRoleId;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UserStoryValidator<UserStoryCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}