namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.Event;
    using ProductBuilder.Application.ViewModels.EventApi;
    using global::AutoMapper;
    using System;

    public class EventAppService : AsdAppService, IEventAppService
    {
        private readonly IEventRepository _repository;

        public EventAppService(IAsdBus bus, IMapper mapper, IEventRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
                throw new ArgumentNullException(nameof(aggregateId));
            return Mapper.Map<AjaxDataTableViewModel>(_repository.Find(x => x.AsdAggregateId == aggregateId));
        }

        public void CreateEvent(CreateEventApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();
            Bus.SendCommand(Mapper.Map<CreateEventCommand>(model));
        }

        public void UpdateEvent(UpdateEventApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateEventCommand>(model));
        }

        public void DeleteEvent(DeleteEventApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<DeleteEventCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}