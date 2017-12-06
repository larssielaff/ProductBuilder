namespace ProductBuilder.Domain.Commands.UserProfile.Base
{
    using Asd.Domain.Core.Commands;
    using System;
    public abstract class UserProfileCommand : AsdCommand
    {
        public Guid Id { get; protected set; }
        public string EmailAddress { get; protected set; }
    }
}