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

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMapForTopic();
            CreateMapForUserProfile();
            CreateMapForProduct();
            CreateMapForTeam();
            CreateMapForTeamMember();
            CreateMapForUserRole();
            CreateMapForEpic();
        }

        private void CreateMapForTopic() { }

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
        }
    }
}