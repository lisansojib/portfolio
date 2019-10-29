using ApplicationCore.Entities.Portfolio;
using AutoMapper;
using Web.Models;

namespace Web.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProjectClient, ProjectClientVeiwModel>();
            CreateMap<ProjectImage, ProjectImageViewModel>();
            CreateMap<Project, ProjectViewModel>();
        }
    }
}
