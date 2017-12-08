namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.Epic;
    using ProductBuilder.Application.ViewModels.EpicApi;
    using global::AutoMapper;
    using System;

    public class EpicAppService : AsdAppService, IEpicAppService
    {
        private readonly IEpicRepository _repository;

        public EpicAppService(IAsdBus bus, IMapper mapper, IEpicRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel()
        {
            return Mapper.Map<AjaxDataTableViewModel>(_repository.GetAll());
        }

        public ProductEpicsDataTableApiViewModel GetProductEpicsDataTableApiViewModel(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));
            return Mapper.Map<ProductEpicsDataTableApiViewModel>(_repository.Find(x => x.ProductId == productId));
        }

        public void DeleteEpic(DeleteEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<DeleteEpicCommand>(model));
        }

        public void CreateEpic(CreateEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            model.Id = Guid.NewGuid();
            Bus.SendCommand(Mapper.Map<CreateEpicCommand>(model));
        }

        public void UpdateEpic(UpdateEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateEpicCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}