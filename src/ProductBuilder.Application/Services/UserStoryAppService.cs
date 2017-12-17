namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.UserStory;
    using ProductBuilder.Application.ViewModels.UserStoryApi;
    using global::AutoMapper;
    using System;
    using System.Linq;
    using ProductBuilder.Application.ViewModels.UserStory;

    public class UserStoryAppService : AsdAppService, IUserStoryAppService
    {
        private readonly IUserStoryRepository _repository;

        public UserStoryAppService(IAsdBus bus, IMapper mapper, IUserStoryRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public ProductUserStoriesDataTableApiViewModel GetProductUserStoriesDataTableApiViewModel(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));
            return Mapper.Map<ProductUserStoriesDataTableApiViewModel>(_repository.Find(x => x.ProductId == productId).ToList());
        }

        public ProductUserStoryViewModel GetProductUserStoryViewModel(Guid productId, Guid userStoryId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));
            if (userStoryId == Guid.Empty)
                throw new ArgumentNullException(nameof(userStoryId));
            return Mapper.Map<ProductUserStoryViewModel>(_repository.Find(x => x.ProductId == productId && x.Id == userStoryId).FirstOrDefault());
        }

        public void AssignTopic(AssignTopicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<AssignTopicCommand>(model));
        }

        public void DeleteUserStory(DeleteUserStoryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<DeleteUserStoryCommand>(model));
        }

        public void CreateUserStory(CreateUserStoryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            model.Id = Guid.NewGuid();
            Bus.SendCommand(Mapper.Map<CreateUserStoryCommand>(model));
        }

        public void RemoveTopic(RemoveTopicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<RemoveTopicCommand>(model));
        }

        public void AssignUserRole(AssignUserRoleApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<AssignUserRoleCommand>(model));
        }

        public void UpdateStoryPoints(UpdateStoryPointsApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateStoryPointsCommand>(model));
        }

        public void UpdateUserStory(UpdateUserStoryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateUserStoryCommand>(model));
        }

        public void AssignEpic(AssignEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<AssignEpicCommand>(model));
        }

        public void UpdateValue(UpdateValueApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateValueCommand>(model));
        }

        public void RemoveUserRole(RemoveUserRoleApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<RemoveUserRoleCommand>(model));
        }

        public void RemoveEpic(RemoveEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<RemoveEpicCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}