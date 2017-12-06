namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.TeamMember;
    using ProductBuilder.Application.ViewModels.TeamMemberApi;
    using global::AutoMapper;
    using System;

    public class TeamMemberAppService : AsdAppService, ITeamMemberAppService
    {
        private readonly ITeamMemberRepository _repository;

        public TeamMemberAppService(IAsdBus bus, IMapper mapper, ITeamMemberRepository repository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel()
        {
            return Mapper.Map<AjaxDataTableViewModel>(_repository.GetAll());
        }

        public void CreateTeamMember(CreateTeamMemberApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Bus.SendCommand(Mapper.Map<CreateTeamMemberCommand>(model));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}