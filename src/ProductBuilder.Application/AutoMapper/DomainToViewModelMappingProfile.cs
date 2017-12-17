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

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMapForUserStory();
            CreateMapForTopic();
            CreateMapForUserProfile();
            CreateMapForProduct();
            CreateMapForTeam();
            CreateMapForTeamMember();
            CreateMapForUserRole();
            CreateMapForEpic();
            CreateMapForAcceptanceCriteria();
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