namespace ProductBuilder.Application.AutoMapper
{
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Domain.Models;
    using global::AutoMapper;
    using System.Collections.Generic;
    using System.Linq;
    using ProductBuilder.Application.ViewModels.ProductApi;
    using ProductBuilder.Application.ViewModels.UserRoleApi;
    using ProductBuilder.Application.ViewModels.EpicApi;
    using ProductBuilder.Application.ViewModels.TopicApi;
    using ProductBuilder.Application.ViewModels.UserStoryApi;
    using ProductBuilder.Application.ViewModels.UserStory;
    using ProductBuilder.Application.ViewModels.Aggregate;
    using ProductBuilder.Application.ViewModels.AggregateApi;
    using ProductBuilder.Application.ViewModels.Query;
    using ProductBuilder.Application.AutoMapper.Extensions;

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMapForCommand();
            CreateMapForUserStory();
            CreateMapForAggregateProperty();
            CreateMapForEvent();
            CreateMapForTopic();
            CreateMapForUserProfile();
            CreateMapForProduct();
            CreateMapForTeam();
            CreateMapForQuery();
            CreateMapForTeamMember();
            CreateMapForUserRole();
            CreateMapForEpic();
            CreateMapForAggregate();
            CreateMapForAcceptanceCriteria();
        }

        private void CreateMapForCommand()
        {
            CreateMap<IEnumerable<Command>, AjaxDataTableViewModel>()
                .ConstructUsing(x => new AjaxDataTableViewModel()
                {
                    Data = x.Select(y => new[]
                    {
                        $"<div class=\"ajax-data-table-Command\" data-Id=\"{y.Id}\" data-CommandName=\"{y.CommandName}\" data-RouteTemplate=\"{y.RouteTemplate}\" data-CommandType=\"{y.CommandType}\">{y.CommandName}</div>",
                        $"<div class=\"ajax-data-table-Command\" data-Id=\"{y.Id}\" data-CommandName=\"{y.CommandName}\" data-RouteTemplate=\"{y.RouteTemplate}\" data-CommandType=\"{y.CommandType}\">{y.RouteTemplate}</div>",
                        $"<div class=\"ajax-data-table-Command\" data-Id=\"{y.Id}\" data-CommandName=\"{y.CommandName}\" data-RouteTemplate=\"{y.RouteTemplate}\" data-CommandType=\"{y.CommandType}\">{y.CommandType}</div>"
                    })
                });
        }

        private void CreateMapForUserStory()
        {
            CreateMap<IEnumerable<UserStory>, ProductUserStoriesDataTableApiViewModel>()
                .ConstructUsing(x => new ProductUserStoriesDataTableApiViewModel()
                {
                    Data = x.Select(y => new string[]
                    {
                        $"<div class=\"ajax-data-table-UserStory\" data-Id=\"{y.Id}\" data-Story=\"{y.Story}\" data-StoryPoints=\"{y.StoryPoints}\" data-Value=\"{y.Value}\" data-Title=\"{y.Title}\" data-UserRoleId=\"{y.UserRoleId}\" data-EpicId=\"{y.EpicId}\" data-TopicId=\"{y.TopicId}\">{y.Title}</div>",
                        $"<div class=\"ajax-data-table-UserStory\" data-Id=\"{y.Id}\" data-Story=\"{y.Story}\" data-StoryPoints=\"{y.StoryPoints}\" data-Value=\"{y.Value}\" data-Title=\"{y.Title}\" data-UserRoleId=\"{y.UserRoleId}\" data-EpicId=\"{y.EpicId}\" data-TopicId=\"{y.TopicId}\">{y.Story}</div>",
                        $"<div class=\"ajax-data-table-UserStory\" data-Id=\"{y.Id}\" data-Story=\"{y.Story}\" data-StoryPoints=\"{y.StoryPoints}\" data-Value=\"{y.Value}\" data-Title=\"{y.Title}\" data-UserRoleId=\"{y.UserRoleId}\" data-EpicId=\"{y.EpicId}\" data-TopicId=\"{y.TopicId}\">{y.StoryPoints}</div>",
                        $"<div class=\"ajax-data-table-UserStory\" data-Id=\"{y.Id}\" data-Story=\"{y.Story}\" data-StoryPoints=\"{y.StoryPoints}\" data-Value=\"{y.Value}\" data-Title=\"{y.Title}\" data-UserRoleId=\"{y.UserRoleId}\" data-EpicId=\"{y.EpicId}\" data-TopicId=\"{y.TopicId}\">{y.Value}</div>"
                    })
                });

            CreateMap<UserStory, ProductUserStoryViewModel>()
                .ConstructUsing(x => new ProductUserStoryViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Story = x.Story,
                    UserRoleId = x.UserRoleId,
                    EpicId = x.EpicId,
                    TopicId = x.TopicId,
                    StoryPoints = x.StoryPoints,
                    Value = x.Value
                });
        }

        private void CreateMapForAggregateProperty()
        {
            CreateMap<IEnumerable<AggregateProperty>, AjaxDataTableViewModel>()
                .ConstructUsing(x => new AjaxDataTableViewModel()
                {
                    Data = x.Select(y => new string[]
                    {
                        $"<div class=\"ajax-data-table-AggregateProperty\" data-Id=\"{y.Id}\" data-Name=\"{y.Name}\" data-Type=\"{y.Type}\" data-LinkedAggregateId=\"{y.LinkedAggregateId}\" data-LinkedAggregateName=\"{y.LinkedAggregateName}\" data-IsAggregateRoot=\"{y.IsAggregateRoot.ToString().ToLower()}\">{y.Name}</div>",
                        $"<div class=\"ajax-data-table-AggregateProperty\" data-Id=\"{y.Id}\" data-Name=\"{y.Name}\" data-Type=\"{y.Type}\" data-LinkedAggregateId=\"{y.LinkedAggregateId}\" data-LinkedAggregateName=\"{y.LinkedAggregateName}\" data-IsAggregateRoot=\"{y.IsAggregateRoot.ToString().ToLower()}\">{y.Type}</div>",
                        $"<div class=\"ajax-data-table-AggregateProperty\" data-Id=\"{y.Id}\" data-Name=\"{y.Name}\" data-Type=\"{y.Type}\" data-LinkedAggregateId=\"{y.LinkedAggregateId}\" data-LinkedAggregateName=\"{y.LinkedAggregateName}\" data-IsAggregateRoot=\"{y.IsAggregateRoot.ToString().ToLower()}\">{y.LinkedAggregate?.Name}</div>",
                        $"<div class=\"ajax-data-table-AggregateProperty\" data-Id=\"{y.Id}\" data-Name=\"{y.Name}\" data-Type=\"{y.Type}\" data-LinkedAggregateId=\"{y.LinkedAggregateId}\" data-LinkedAggregateName=\"{y.LinkedAggregateName}\" data-IsAggregateRoot=\"{y.IsAggregateRoot.ToString().ToLower()}\">{y.LinkedAggregateName}</div>",
                        $"<div class=\"ajax-data-table-AggregateProperty\" data-Id=\"{y.Id}\" data-Name=\"{y.Name}\" data-Type=\"{y.Type}\" data-LinkedAggregateId=\"{y.LinkedAggregateId}\" data-LinkedAggregateName=\"{y.LinkedAggregateName}\" data-IsAggregateRoot=\"{y.IsAggregateRoot.ToString().ToLower()}\">{y.IsAggregateRoot}</div>"
                    })
                });
        }

        private void CreateMapForEvent()
        {
            CreateMap<IEnumerable<Event>, AjaxDataTableViewModel>()
                .ConstructUsing(x => new AjaxDataTableViewModel()
                {
                    Data = x.Select(y => new [] 
                    {
                        $"<div class=\"ajax-data-table-Event\" data-Id=\"{y.Id}\" data-EventName=\"{y.EventName}\">{y.EventName}</div>"
                    })
                });
        }

        private void CreateMapForTopic()
        {
            CreateMap<IEnumerable<Topic>, ProductTopicsDataTableApiViewModel>()
                .ConstructUsing(x => new ProductTopicsDataTableApiViewModel()
                {
                    Data = x.Select(y => new string[]
                    {
                        $"<div class=\"ajax-data-table-Topic\" data-Id=\"{y.Id}\" data-Title=\"{y.Title}\" data-Description=\"{y.Description}\">{y.Title}</div>",
                        $"<div class=\"ajax-data-table-Topic\" data-Id=\"{y.Id}\" data-Title=\"{y.Title}\" data-Description=\"{y.Description}\">{y.Description}</div>"
                    })
                });

            CreateMap<IEnumerable<Topic>, IEnumerable<TopicQueryResult>>()
                .ConstructUsing(x => x.Select(y => new TopicQueryResult()
                {
                    Id = y.Id,
                    Title = y.Title
                }));
        }

        private void CreateMapForUserProfile()
        {
            CreateMap<IEnumerable<UserProfile>, AjaxDataTableViewModel>()
                .ForMember(x => x.Data, y => y.MapFrom(x => x.Select(z => new string[] 
                {
                    $"<div class=\"ajax-data-table-UserProfile\" data-Id=\"{z.Id}\" data-EmailAddress=\"{z.EmailAddress}\">{z.EmailAddress}</div>",
                })));
        }

        private void CreateMapForProduct()
        {
            CreateMap<IEnumerable<Product>, AjaxDataTableViewModel>()
                .ForMember(x => x.Data, y => y.MapFrom(x => x.Select(z => new string[] 
                {
                    $"<div class=\"ajax-data-table-Product\" data-Id=\"{z.Id}\" data-Title=\"{z.Title}\" data-ProductVision=\"{z.ProductVision}\">{z.Title}</div>", $"<div class=\"ajax-data-table-Product\" data-Id=\"{z.Id}\" data-Title=\"{z.Title}\" data-ProductVision=\"{z.ProductVision}\">{z.ProductVision}</div>",
                })));
        }

        private void CreateMapForTeam()
        {
            CreateMap<IEnumerable<Team>, AjaxDataTableViewModel>()
                .ForMember(x => x.Data, y => y.MapFrom(x => x.Select(z => new string[] 
                {
                    $"<div class=\"ajax-data-table-Team\" data-Id=\"{z.Id}\" data-Title=\"{z.Title}\">{z.Title}</div>",
                })));
        }

        private void CreateMapForQuery()
        {
            CreateMap<IEnumerable<Query>, AjaxDataTableViewModel>()
                .ConstructUsing(x => new AjaxDataTableViewModel()
                {
                    Data = x.Select(y => new string[] 
                    {
                        $"<div class=\"ajax-data-table-Query\" data-Id=\"{y.Id}\" data-QueryName=\"{y.QueryName}\" data-RouteTemplate=\"{y.RouteTemplate}\">{y.QueryName}</div>",
                        $"<div class=\"ajax-data-table-Query\" data-Id=\"{y.Id}\" data-QueryName=\"{y.QueryName}\" data-RouteTemplate=\"{y.RouteTemplate}\">{y.RouteTemplate}</div>"
                    })
                });

            CreateMap<Query, QueryViewModel>()
                .ConstructUsing(x => new QueryViewModel()
                {
                    Id = x.Id,
                    QueryName = x.QueryName,
                    RouteTemplate = x.RouteTemplate,
                    AsdAggregateId = x.AsdAggregateId.Value
                });
        }

        private void CreateMapForTeamMember()
        {
            CreateMap<IEnumerable<TeamMember>, AjaxDataTableViewModel>()
                .ForMember(x => x.Data, y => y.MapFrom(x => x.Select(z => new string[] 
                {
                    $"<div class=\"ajax-data-table-TeamMember\" data-Id=\"{z.Id}\" data-Role=\"{z.Role}\">{z.Role}</div>",
                })));

            CreateMap<IEnumerable<TeamMember>, ProductTeamMembersApiViewModel>()
                .ConstructUsing(x => new ProductTeamMembersApiViewModel()
                {
                    Data = x.Select(y => new string[] 
                    {
                        $"<div class=\"ajax-data-table-TeamMember\" data-Id=\"{y.Id}\" data-UserProfile-EmailAddress=\"{y.UserProfile?.EmailAddress}\" data-Team-Title=\"{y.Team.Title}\" data-Role=\"{y.Role}\">{y.UserProfile?.EmailAddress}</div>",
                        $"<div class=\"ajax-data-table-TeamMember\" data-Id=\"{y.Id}\" data-UserProfile-EmailAddress=\"{y.UserProfile?.EmailAddress}\" data-Team-Title=\"{y.Team.Title}\" data-Role=\"{y.Role}\">{y.Team?.Title}</div>",
                        $"<div class=\"ajax-data-table-TeamMember\" data-Id=\"{y.Id}\" data-UserProfile-EmailAddress=\"{y.UserProfile?.EmailAddress}\" data-Team-Title=\"{y.Team.Title}\" data-Role=\"{y.Role}\">{y.Role}</div>",
                    })
                });
        }

        private void CreateMapForUserRole()
        {
            CreateMap<IEnumerable<UserRole>, ProductUserRolesApiViewModel>()
                .ConstructUsing(x => new ProductUserRolesApiViewModel()
                {
                    Data = x.Select(y => new string[] 
                    {
                        $"<div class=\"ajax-data-table-UserRole\" data-Id=\"{y.Id}\" data-Role=\"{y.Role}\">{y.Role}</div>"
                    })
                });

            CreateMap<IEnumerable<UserRole>, IEnumerable<UserRoleQueryResult>>()
                .ConstructUsing(x => x.Select(y => new UserRoleQueryResult()
                {
                    Id = y.Id,
                    Role = y.Role
                }));
        }

        private void CreateMapForEpic()
        {
            CreateMap<IEnumerable<Epic>, ProductEpicsDataTableApiViewModel>()
                .ConstructUsing(x => new ProductEpicsDataTableApiViewModel()
                {
                    Data = x.Select(y => new string[]
                    {
                        $"<div class=\"ajax-data-table-Epic\" data-Id=\"{y.Id}\" data-Title=\"{y.Title}\" data-Description=\"{y.Description}\">{y.Title}</div>",
                        $"<div class=\"ajax-data-table-Epic\" data-Id=\"{y.Id}\" data-Title=\"{y.Title}\" data-Description=\"{y.Description}\">{y.Description}</div>"
                    })
                });

            CreateMap<IEnumerable<Epic>, IEnumerable<EpicQueryResult>>()
                .ConstructUsing(x => x.Select(y => new EpicQueryResult()
                {
                    Id = y.Id,
                    Title = y.Title
                }));
        }

        private void CreateMapForAggregate()
        {
            CreateMap<IEnumerable<Aggregate>, AjaxDataTableViewModel>()
                .ConstructUsing(x => new AjaxDataTableViewModel()
                {
                    Data = x.Select(y => new string[]
                    {
                        $"<div class=\"ajax-data-table-Aggregate\" data-Id=\"{y.Id}\" data-Name=\"{y.Name}\" data-NamePluralized=\"{y.NamePluralized}\">{y.Name}</div>",
                        $"<div class=\"ajax-data-table-Aggregate\" data-Id=\"{y.Id}\" data-Name=\"{y.Name}\" data-NamePluralized=\"{y.NamePluralized}\">{y.NamePluralized}</div>"
                    })
                });

            CreateMap<Aggregate, AggregateViewModel>()
                .ConstructUsing(x => new AggregateViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NamePluralized = x.NamePluralized
                });

            CreateMap<IEnumerable<Aggregate>, IEnumerable<AggregateQueryResult>>()
                .ConstructUsing(x => x.Select(y => new AggregateQueryResult()
                {
                    Id = y.Id,
                    Name = y.Name
                }));

            CreateMap<IEnumerable<Aggregate>, VisJsNetworkApiViewModel>()
                .ConstructUsing(x => new VisJsNetworkApiViewModel()
                {
                    Nodes = x.Select(y => new VisJsNetworkNodeApiViewModel()
                    {
                        Id = y.Id.ToString(),
                        Label = y.Name
                    }),
                    Edges = x.SelectMany(y => y.LinkedAggregateProperties
                        .Where(z => z.LinkedAggregateId != null && !z.Deleted))
                        .Select(y => new VisJsNetworkEdgeApiViewModel()
                        {
                            From = y.AsdAggregateId.ToString(),
                            To = y.LinkedAggregateId.ToString(),
                            Arrows = "to",
                            Dashes = y.IsAggregateRoot
                        })
                });

            CreateMap<Aggregate, AggregateCodeViewModel>()
                .ConstructUsing(x => new AggregateCodeViewModel()
                {
                    AggregateClassName = x.Name,
                    AggregateCode = x.ToAggregateCode(),
                    BaseCommandClassName = $"{x.Name}Command",
                    BaseCommandCode = x.ToBaseCommandCode(),
                    AggregateValidatorClassName = $"{x.Name}Validator",
                    AggregateValidatorCode = x.ToAggregateValidatorCode(),
                    IAggregateRepositoryInterfaceName = $"I{x.Name}Repository",
                    IAggregateRepositoryCode = x.ToIAggregateRepositoryCode(),
                    AggregateRepositoryClasseName = $"{x.Name}Repository",
                    AggregateRepositoryCode = x.ToAggregateRepositoryCode(),
                    DomainEvents = x.Events
                        .Where(y => !y.Deleted)
                        .ToDictionary(y => $"{y.EventName}Event", y => y.ToDomainEventCode())
                });
        }

        private void CreateMapForAcceptanceCriteria()
        {
            CreateMap<IEnumerable<AcceptanceCriteria>, AjaxDataTableViewModel>()
                .ConstructUsing(x => new AjaxDataTableViewModel()
                {
                    Data = x.Select(y => new string[] 
                    {
                        $"<div class=\"ajax-data-table-AcceptanceCriteria\" data-Id=\"{y.Id}\" data-Title=\"{y.Title}\">{y.Title}</div>"
                    })
                });
        }
    }
}