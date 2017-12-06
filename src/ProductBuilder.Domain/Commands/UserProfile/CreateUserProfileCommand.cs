namespace ProductBuilder.Domain.Commands.UserProfile
{
    using ProductBuilder.Domain.Commands.UserProfile.Base;
    using ProductBuilder.Domain.Validations.UserProfile;
    using System;
    public class CreateUserProfileCommand : UserProfileCommand
    {
        public CreateUserProfileCommand(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
        public override bool IsValid()
        {
            ValidationResult = new UserProfileValidator<UserProfileCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}