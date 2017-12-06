namespace ProductBuilder.Domain.Validations.TeamMember
{
    using ProductBuilder.Domain.Commands.TeamMember.Base;
    using FluentValidation;
    public class TeamMemberValidator<T> : AbstractValidator<T> where T : TeamMemberCommand { }
}
