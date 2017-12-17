namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.Aggregate;
    using ProductBuilder.Application.ViewModels.AggregateApi;
    using global::AutoMapper;
    using System;

    public class AggregateAppService : AsdAppService, IAggregateAppService
    {
        private readonly IAggregateRepository _repository;

        public AggregateAppService(IAsdBus bus, IMapper mapper, IAggregateRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel()
        {
            return Mapper.Map<AjaxDataTableViewModel>(_repository.GetAll());
        }

        public void DeleteAggregate(DeleteAggregateApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<DeleteAggregateCommand>(model));
        }

        public void CreateAggregate(CreateAggregateApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<CreateAggregateCommand>(model));
        }

        public void UpdateAggregate(UpdateAggregateApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateAggregateCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}