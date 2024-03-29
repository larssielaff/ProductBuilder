﻿namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.AcceptanceCriteria;
    using ProductBuilder.Application.ViewModels.AcceptanceCriteriaApi;
    using global::AutoMapper;
    using System;
    using System.Linq;

    public class AcceptanceCriteriaAppService : AsdAppService, IAcceptanceCriteriaAppService
    {
        private readonly IAcceptanceCriteriaRepository _repository;

        public AcceptanceCriteriaAppService(IAsdBus bus, IMapper mapper, IAcceptanceCriteriaRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetUserStoryAcceptanceCriteriasDataTable(Guid userStoryId)
        {
            if (userStoryId == Guid.Empty)
                throw new ArgumentNullException(nameof(userStoryId));
            return Mapper.Map<AjaxDataTableViewModel>(_repository.Find(x => x.UserStoryId == userStoryId).ToList());
        }

        public void CreateAcceptanceCriteria(CreateAcceptanceCriteriaApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            model.Id = Guid.NewGuid();
            Bus.SendCommand(Mapper.Map<CreateAcceptanceCriteriaCommand>(model));
        }

        public void UpdateAcceptanceCriteria(UpdateAcceptanceCriteriaApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<UpdateAcceptanceCriteriaCommand>(model));
        }

        public void DeleteAcceptanceCriteria(DeleteAcceptanceCriteriaApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<DeleteAcceptanceCriteriaCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}