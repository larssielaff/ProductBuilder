namespace ProductBuilder.Application.Services
{
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.UserProfile;
    using ProductBuilder.Application.ViewModels.UserProfileApi;
    using global::AutoMapper;
    using System;
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;

    public class UserProfileAppService : AsdAppService, IUserProfileAppService
    {
        private readonly IUserProfileRepository _repository;
        public UserProfileAppService(IAsdBus bus, IMapper mapper, IUserProfileRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public AjaxDataTableViewModel GetDataTableViewModel()
        {
            return Mapper.Map<AjaxDataTableViewModel>(_repository.GetAll());
        }
        public void CreateUserProfile(CreateUserProfileApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<CreateUserProfileCommand>(model));
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}