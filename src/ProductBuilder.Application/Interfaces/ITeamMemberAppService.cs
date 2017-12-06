namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.TeamMemberApi;

    public interface ITeamMemberAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();
        void CreateTeamMember(CreateTeamMemberApiViewModel model);
    }
}
