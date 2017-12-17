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
    using ProductBuilder.Application.ViewModels.UserRoleApi;
    using ProductBuilder.Domain.Commands.UserRole;
    using ProductBuilder.Application.ViewModels.EpicApi;
    using ProductBuilder.Domain.Commands.Epic;
    using ProductBuilder.Application.ViewModels.TopicApi;
    using ProductBuilder.Domain.Commands.Topic;
    using ProductBuilder.Application.ViewModels.UserStoryApi;
    using ProductBuilder.Domain.Commands.UserStory;
    using ProductBuilder.Application.ViewModels.AcceptanceCriteriaApi;
    using ProductBuilder.Domain.Commands.AcceptanceCriteria;

    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMapForUserStory();
            CreateMapForTopic();
            CreateMapForUserProfile();
            CreateMapForProduct();
            CreateMapForTeam();
            CreateMapForTeamMember();
            CreateMapForUserRole();
            CreateMapForEpic();
            CreateMapForAggregate();
            CreateMapForAcceptanceCriteria();
        }

        private void CreateMapForUserStory()
        {
            CreateMap<CreateUserStoryApiViewModel, CreateUserStoryCommand>()
                .ConstructUsing(x => new CreateUserStoryCommand(x.Id, x.Title, x.Story, x.ProductId, x.ProductId));

            CreateMap<DeleteUserStoryApiViewModel, DeleteUserStoryCommand>()
                .ConstructUsing(x => new DeleteUserStoryCommand(x.Id, x.ProductId));

            CreateMap<UpdateUserStoryApiViewModel, UpdateUserStoryCommand>()
                .ConstructUsing(x => new UpdateUserStoryCommand(x.Id, x.Title, x.Story, x.ProductId));

            CreateMap<AssignUserRoleApiViewModel, AssignUserRoleCommand>()
                .ConstructUsing(x => new AssignUserRoleCommand(x.Id, x.UserRoleId, x.ProductId));

            CreateMap<AssignEpicApiViewModel, AssignEpicCommand>()
                .ConstructUsing(x => new AssignEpicCommand(x.Id, x.EpicId, x.ProductId));

            CreateMap<AssignTopicApiViewModel, AssignTopicCommand>()
                .ConstructUsing(x => new AssignTopicCommand(x.Id, x.TopicId, x.ProductId));

            CreateMap<UpdateStoryPointsApiViewModel, UpdateStoryPointsCommand>()
                .ConstructUsing(x => new UpdateStoryPointsCommand(x.Id, x.StoryPoints, x.ProductId));

            CreateMap<UpdateValueApiViewModel, UpdateValueCommand>()
                .ConstructUsing(x => new UpdateValueCommand(x.Id, x.Value, x.ProductId));

            CreateMap<RemoveUserRoleApiViewModel, RemoveUserRoleCommand>()
                .ConstructUsing(x => new RemoveUserRoleCommand(x.Id, x.ProductId));

            CreateMap<RemoveEpicApiViewModel, RemoveEpicCommand>()
                .ConstructUsing(x => new RemoveEpicCommand(x.Id, x.ProductId));

            CreateMap<RemoveTopicApiViewModel, RemoveTopicCommand>()
                .ConstructUsing(x => new RemoveTopicCommand(x.Id, x.ProductId));
        }

        private void CreateMapForTopic()
        {
            CreateMap<CreateTopicApiViewModel, CreateTopicCommand>()
                .ConstructUsing(x => new CreateTopicCommand(x.Id, x.Title, x.Description, x.ProductId));

            CreateMap<DeleteTopicApiViewModel, DeleteTopicCommand>()
                .ConstructUsing(x => new DeleteTopicCommand(x.Id, x.ProductId));

            CreateMap<UpdateTopicApiViewModel, UpdateTopicCommand>()
                .ConstructUsing(x => new UpdateTopicCommand(x.Id, x.Title, x.Description, x.ProductId));
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

        private void CreateMapForUserRole()
        {
            CreateMap<CreateUserRoleApiViewModel, CreateUserRoleCommand>()
                .ConstructUsing(x => new CreateUserRoleCommand(x.Id, x.Role, x.ProductId));

            CreateMap<DeleteUserRoleApiViewModel, DeleteUserRoleCommand>()
                .ConstructUsing(x => new DeleteUserRoleCommand(x.Id, x.ProductId));

            CreateMap<UpdateUserRoleApiViewModel, UpdateUserRoleCommand>()
                .ConstructUsing(x => new UpdateUserRoleCommand(x.Id, x.Role, x.ProductId));
        }

        private void CreateMapForEpic()
        {
            CreateMap<CreateEpicApiViewModel, CreateEpicCommand>()
                .ConstructUsing(x => new CreateEpicCommand(x.Id, x.Title, x.Description, x.ProductId, x.ProductId));

            CreateMap<DeleteEpicApiViewModel, DeleteEpicCommand>()
                .ConstructUsing(x => new DeleteEpicCommand(x.Id, x.ProductId));

            CreateMap<UpdateEpicApiViewModel, UpdateEpicCommand>()
                .ConstructUsing(x => new UpdateEpicCommand(x.Id, x.Title, x.Description, x.ProductId));
        }

        private void CreateMapForAggregate() { }

        private void CreateMapForAcceptanceCriteria()
        {
            CreateMap<CreateAcceptanceCriteriaApiViewModel, CreateAcceptanceCriteriaCommand>()
                .ConstructUsing(x => new CreateAcceptanceCriteriaCommand(x.Id, x.Title, x.UserStoryId, x.UserStoryId));

            CreateMap<DeleteAcceptanceCriteriaApiViewModel, DeleteAcceptanceCriteriaCommand>()
                .ConstructUsing(x => new DeleteAcceptanceCriteriaCommand(x.Id, x.UserStoryId));

            CreateMap<UpdateAcceptanceCriteriaApiViewModel, UpdateAcceptanceCriteriaCommand>()
                .ConstructUsing(x => new UpdateAcceptanceCriteriaCommand(x.Id, x.Title, x.UserStoryId));
        }
    }
}