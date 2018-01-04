namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.Command;
    using ProductBuilder.Application.ViewModels.CommandApi;
    using global::AutoMapper;
    using System;

    public class CommandAppService : AsdAppService, ICommandAppService
    {
        private readonly ICommandRepository _repository;

        public CommandAppService(IAsdBus bus, IMapper mapper, ICommandRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel()
        {
            return Mapper.Map<AjaxDataTableViewModel>(_repository.GetAll());
        }

        public void UpdateCommand(UpdateCommandApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateCommandCommand>(model));
        }

        public void DeleteCommand(DeleteCommandApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<DeleteCommandCommand>(model));
        }

        public void CreateCommand(CreateCommandApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<CreateCommandCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}