namespace ProductBuilder.Application.Services
{
    using Asd.Application.Services;
    using Asd.Domain.Core.Bus;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Commands.Product;
    using ProductBuilder.Application.ViewModels.ProductApi;
    using global::AutoMapper;
    using System;
    using ProductBuilder.Domain.Commands.Team;
    using ProductBuilder.Application.ViewModels.TeamApi;
    using ProductBuilder.Domain.Commands.TeamMember;
    using ProductBuilder.Application.ViewModels.TeamMemberApi;
    using System.Linq;

    public class ProductAppService : AsdAppService, IProductAppService
    {
        private readonly IProductRepository _repository;
        private readonly IUserProfileRepository _userProfileRepository;

        public ProductAppService(IAsdBus bus, IMapper mapper, IProductRepository repository, IUserProfileRepository userProfileRepository) 
            : base(bus, mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        }

        public AjaxDataTableViewModel GetDataTableViewModel()
        {
            return Mapper.Map<AjaxDataTableViewModel>(_repository.GetAll());
        }

        public ProductTeamMembersApiViewModel GetProductTeamMembersApiViewModel(Guid productId)
        {
            if (productId == null)
                throw new ArgumentNullException(nameof(productId));
            return Mapper.Map<ProductTeamMembersApiViewModel>(_repository.GetById(productId)?.Teams?.FirstOrDefault()?.TeamMembers);
        }

        public void CreateProduct(CreateProductApiViewModel model, string userProfileEmailAddress)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrWhiteSpace(userProfileEmailAddress))
                throw new ArgumentNullException(nameof(userProfileEmailAddress));

            var productId = Guid.NewGuid();
            var teamId = Guid.NewGuid();
            var teamMemberId = Guid.NewGuid();
            var userProfileId = _userProfileRepository
                .Find(x => x.EmailAddress.ToLower() == userProfileEmailAddress.ToLower())?
                .FirstOrDefault()?
                .Id ?? Guid.Empty;
            if (userProfileId == Guid.Empty)
                throw new NullReferenceException(nameof(userProfileId));

            model.Id = productId;
            Bus.SendCommand(Mapper.Map<CreateProductCommand>(model));
            Bus.SendCommand(Mapper.Map<CreateTeamCommand>(new CreateTeamApiViewModel()
            {
                Id = teamId,
                ProductId = productId,
                Title = $"{model.Title} - Team"
            }));
            Bus.SendCommand(Mapper.Map<CreateTeamMemberCommand>(new CreateTeamMemberApiViewModel()
            {
                Id = teamMemberId,
                Role = "Product Owner",
                TeamId = teamId,
                UserProfileId = userProfileId
            }));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}