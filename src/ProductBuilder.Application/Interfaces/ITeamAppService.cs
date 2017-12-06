namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.TeamApi;

    public interface ITeamAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();
        void CreateTeam(CreateTeamApiViewModel model);
    }
}