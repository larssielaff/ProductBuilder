﻿namespace ProductBuilder.Application.Services
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
    using ProductBuilder.Application.ViewModels.Command;

    public class CommandAppService : AsdAppService, ICommandAppService
    {
        private readonly ICommandRepository _repository;

        public CommandAppService(IAsdBus bus, IMapper mapper, ICommandRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
                throw new ArgumentNullException(nameof(aggregateId));
            return Mapper.Map<AjaxDataTableViewModel>(_repository.Find(x => x.DomainAggregateId == aggregateId));
        }

        public DomainCommandViewModel GetDomainCommandViewModel(Guid commandId)
        {
            if (commandId == Guid.Empty)
                throw new ArgumentNullException(nameof(commandId));
            return Mapper.Map<DomainCommandViewModel>(_repository.GetById(commandId));
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
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();
            Bus.SendCommand(Mapper.Map<CreateCommandCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}