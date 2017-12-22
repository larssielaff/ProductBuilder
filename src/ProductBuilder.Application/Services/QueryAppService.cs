namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.Query;
    using ProductBuilder.Application.ViewModels.QueryApi;
    using global::AutoMapper;
    using System;

    public class QueryAppService : AsdAppService, IQueryAppService
    {
        private readonly IQueryRepository _repository;

        public QueryAppService(IAsdBus bus, IMapper mapper, IQueryRepository repository) 
            : base(bus, mapper) { _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel()
        {
            return Mapper.Map<AjaxDataTableViewModel>(_repository.GetAll());
        }

        public void CreateQuery(CreateQueryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<CreateQueryCommand>(model));
        }

        public void UpdateQuery(UpdateQueryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateQueryCommand>(model));
        }

        public void DeleteQuery(DeleteQueryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<DeleteQueryCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}