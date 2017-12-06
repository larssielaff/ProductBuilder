namespace ProductBuilder.Domain.Commands.Team
{
    using ProductBuilder.Domain.Commands.Team.Base;
    using ProductBuilder.Domain.Validations.Team;
    using System;

    public class CreateTeamCommand : TeamCommand
    {
        public CreateTeamCommand(Guid id, string title, Guid productId, Guid aggregateId)
        {
            Id = id;
            Title = title;
            ProductId = productId;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new TeamValidator<TeamCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}