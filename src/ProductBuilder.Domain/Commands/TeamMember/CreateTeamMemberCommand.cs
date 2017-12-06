namespace ProductBuilder.Domain.Commands.TeamMember
{
    using ProductBuilder.Domain.Commands.TeamMember.Base;
    using ProductBuilder.Domain.Validations.TeamMember;
    using System;

    public class CreateTeamMemberCommand : TeamMemberCommand
    {
        public CreateTeamMemberCommand(Guid id, string role, Guid userProfileId, Guid teamId, Guid aggregateId)
        {
            Id = id;
            Role = role;
            UserProfileId = userProfileId;
            TeamId = teamId;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new TeamMemberValidator<TeamMemberCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}