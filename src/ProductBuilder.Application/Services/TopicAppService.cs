namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.Topic;
    using ProductBuilder.Application.ViewModels.TopicApi;
    using global::AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TopicAppService : AsdAppService, ITopicAppService
    {
        private readonly ITopicRepository _repository;

        public TopicAppService(IAsdBus bus, IMapper mapper, ITopicRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public ProductTopicsDataTableApiViewModel GetProductTopicsDataTableApiViewModel(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));
            return Mapper.Map<ProductTopicsDataTableApiViewModel>(_repository.Find(x => x.ProductId == productId));
        }

        public AjaxDataTableViewModel GetDataTableViewModel()
        {
            return Mapper.Map<AjaxDataTableViewModel>(_repository.GetAll());
        }

        public IEnumerable<TopicQueryResult> GetProductTopicsJsonArray(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));
            return Mapper.Map<IEnumerable<TopicQueryResult>>(_repository.Find(x => x.ProductId == productId).ToList());
        }

        public void DeleteTopic(DeleteTopicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<DeleteTopicCommand>(model));
        }

        public void CreateTopic(CreateTopicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            model.Id = Guid.NewGuid();
            Bus.SendCommand(Mapper.Map<CreateTopicCommand>(model));
        }

        public void UpdateTopic(UpdateTopicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateTopicCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}