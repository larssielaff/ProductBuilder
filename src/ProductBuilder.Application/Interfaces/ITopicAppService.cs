namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.TopicApi;
    using System.Collections.Generic;

    public interface ITopicAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        IEnumerable<TopicQueryResult> GetProductTopicsJsonArray(Guid productId);

        void DeleteTopic(DeleteTopicApiViewModel model);

        void CreateTopic(CreateTopicApiViewModel model);

        void UpdateTopic(UpdateTopicApiViewModel model);
        ProductTopicsDataTableApiViewModel GetProductTopicsDataTableApiViewModel(Guid productId);
    }
}