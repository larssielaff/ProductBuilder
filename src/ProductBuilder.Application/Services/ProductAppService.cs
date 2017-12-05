namespace ProductBuilder.Application.Services
{
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using global::AutoMapper;
    using System;
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;

    public class ProductAppService : AsdAppService, IProductAppService
    {
        private readonly IProductRepository _repository;
        public ProductAppService(IAsdBus bus, IMapper mapper, IProductRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public AjaxDataTableViewModel GetDataTableViewModel()
        {
            return Mapper.Map<AjaxDataTableViewModel>(_repository.GetAll());
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}