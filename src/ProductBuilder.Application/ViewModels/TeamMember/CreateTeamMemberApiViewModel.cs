namespace ProductBuilder.Application.ViewModels.TeamMemberApi
{
    using System;
    public class CreateTeamMemberApiViewModel
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
        public Guid TeamId { get; set; }
        public string Role { get; set; }
    }
}