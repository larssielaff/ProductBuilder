namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.AggregateProperty;
    using ProductBuilder.Application.ViewModels.AggregatePropertyApi;
    using global::AutoMapper;
    using System;
    using System.Linq;

    public class AggregatePropertyAppService : AsdAppService, IAggregatePropertyAppService
    {
        private readonly IAggregatePropertyRepository _repository;

        public AggregatePropertyAppService(IAsdBus bus, IMapper mapper, IAggregatePropertyRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel(Guid asdAggregateId)
        {
            if (asdAggregateId == Guid.Empty)
                throw new ArgumentNullException(nameof(asdAggregateId));
            return Mapper.Map<AjaxDataTableViewModel>(_repository.Find(x => x.AsdAggregateId == asdAggregateId).ToList());
        }

        public void UpdateAggregate(UpdateAggregatePropertyApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateAggregatePropertyCommand>(model));
        }

        public void DeleteAggregate(DeleteAggregatePropertyApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<DeleteAggregatePropertyCommand>(model));
        }

        public void CreateAggregate(CreateAggregatePropertyApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();
            Bus.SendCommand(Mapper.Map<CreateAggregatePropertyCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}