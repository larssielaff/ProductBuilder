namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.TopicApi;

    public interface ITopicAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        void DeleteTopic(DeleteTopicApiViewModel model);

        void CreateTopic(CreateTopicApiViewModel model);

        void UpdateTopic(UpdateTopicApiViewModel model);
    }
}