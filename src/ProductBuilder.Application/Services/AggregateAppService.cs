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
    using System.Linq;
    using ProductBuilder.Application.ViewModels.Aggregate;
    using System.Collections.Generic;

    public class AggregateAppService : AsdAppService, IAggregateAppService
    {
        private readonly IAggregateRepository _repository;

        public AggregateAppService(IAsdBus bus, IMapper mapper, IAggregateRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));
            return Mapper.Map<AjaxDataTableViewModel>(_repository.Find(x => x.ProductId == productId).ToList());
        }

        public AggregateViewModel GetAggregateViewModel(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
                throw new ArgumentNullException(nameof(aggregateId));
            return Mapper.Map<AggregateViewModel>(_repository.GetById(aggregateId));
        }

        public IEnumerable<AggregateQueryResult> GetProductAggregatesJsonArray(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));
            return Mapper.Map<IEnumerable<AggregateQueryResult>>(_repository.Find(x => x.ProductId == productId).ToList());
        }

        public VisJsNetworkApiViewModel GetAggregateMapQueryResult(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));
            return Mapper.Map<VisJsNetworkApiViewModel>(_repository.Find(x => x.ProductId == productId).ToList());
        }

        public AggregateCodeViewModel GetAggregateCodeViewModel(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
                throw new ArgumentNullException(nameof(aggregateId));
            return Mapper.Map<AggregateCodeViewModel>(_repository.GetById(aggregateId));
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
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();
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