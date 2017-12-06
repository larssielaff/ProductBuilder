namespace ProductBuilder.Application.AutoMapper
{
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Models;
    using global::AutoMapper;
    using System.Collections.Generic;
    using System.Linq;
    using ProductBuilder.Application.ViewModels.ProductApi;
    using ProductBuilder.Domain.Commands.Product;
    using ProductBuilder.Application.ViewModels.TeamApi;
    using ProductBuilder.Domain.Commands.Team;
    using ProductBuilder.Application.ViewModels.TeamMemberApi;
    using ProductBuilder.Domain.Commands.TeamMember;

    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMapForUserProfile();
            CreateMapForProduct();
            CreateMapForTeam();
            CreateMapForTeamMember();
        }

        private void CreateMapForUserProfile() { }

        private void CreateMapForProduct()
        {
            CreateMap<CreateProductApiViewModel, CreateProductCommand>()
                .ConstructUsing(x => new CreateProductCommand(x.Id, x.Title, x.Id));
        }

        private void CreateMapForTeam()
        {
            CreateMap<CreateTeamApiViewModel, CreateTeamCommand>()
                .ConstructUsing(x => new CreateTeamCommand(x.Id, x.Title, x.ProductId, x.ProductId));
        }

        private void CreateMapForTeamMember()
        {
            CreateMap<CreateTeamMemberApiViewModel, CreateTeamMemberCommand>()
                .ConstructUsing(x => new CreateTeamMemberCommand(x.Id, x.Role, x.UserProfileId, x.TeamId, x.TeamId));
        }
    }
}