namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.UserStoryApi;

    public interface IUserStoryAppService : IDisposable
    {
        ProductUserStoriesDataTableApiViewModel GetProductUserStoriesDataTableApiViewModel(Guid productId);

        void AssignTopic(AssignTopicApiViewModel model);

        void DeleteUserStory(DeleteUserStoryApiViewModel model);

        void CreateUserStory(CreateUserStoryApiViewModel model);

        void RemoveTopic(RemoveTopicApiViewModel model);

        void AssignUserRole(AssignUserRoleApiViewModel model);

        void UpdateStoryPoints(UpdateStoryPointsApiViewModel model);

        void UpdateUserStory(UpdateUserStoryApiViewModel model);

        void AssignEpic(AssignEpicApiViewModel model);

        void UpdateValue(UpdateValueApiViewModel model);

        void RemoveUserRole(RemoveUserRoleApiViewModel model);

        void RemoveEpic(RemoveEpicApiViewModel model);
    }
}