namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.DomainCommandArgument;
    using ProductBuilder.Application.ViewModels.DomainCommandArgumentApi;
    using global::AutoMapper;
    using System;

    public class DomainCommandArgumentAppService : AsdAppService, IDomainCommandArgumentAppService
    {
        private readonly IDomainCommandArgumentRepository _repository;

        public DomainCommandArgumentAppService(IAsdBus bus, IMapper mapper, IDomainCommandArgumentRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel(Guid commandId)
        {
            if (commandId == Guid.Empty)
                throw new ArgumentNullException(nameof(commandId));
            return Mapper.Map<AjaxDataTableViewModel>(_repository.Find(x => x.DomainCommandId == commandId));
        }

        public void UpdateDomainCommandArgument(UpdateDomainCommandArgumentApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateDomainCommandArgumentCommand>(model));
        }

        public void CreateDomainCommandArgument(CreateDomainCommandArgumentApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();
            Bus.SendCommand(Mapper.Map<CreateDomainCommandArgumentCommand>(model));
        }

        public void DeleteDomainCommandArgument(DeleteDomainCommandArgumentApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<DeleteDomainCommandArgumentCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}