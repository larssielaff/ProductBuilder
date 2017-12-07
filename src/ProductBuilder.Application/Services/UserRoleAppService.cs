namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.UserRole;
    using ProductBuilder.Application.ViewModels.UserRoleApi;
    using global::AutoMapper;
    using System;

    public class UserRoleAppService : AsdAppService, IUserRoleAppService
    {
        private readonly IUserRoleRepository _repository;

        public UserRoleAppService(IAsdBus bus, IMapper mapper, IUserRoleRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel()
        {
            return Mapper.Map<AjaxDataTableViewModel>(_repository.GetAll());
        }

        public void CreateUserRole(CreateUserRoleApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<CreateUserRoleCommand>(model));
        }

        public void DeleteUserRole(DeleteUserRoleApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<DeleteUserRoleCommand>(model));
        }

        public void UpdateUserRole(UpdateUserRoleApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateUserRoleCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}