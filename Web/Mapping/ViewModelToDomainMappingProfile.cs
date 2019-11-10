using ApplicationCore.Entities.Portfolio;
using AutoMapper;
using Web.Models;

namespace Web.Mapping
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProjectImageBindingModel, ProjectImage>();
            CreateMap<ProjectBindingModel, Project>()
                .ForMember(dest => dest.ProjectClients, src => src.MapFrom(x => x.ProjectClients))
                .ForMember(dest => dest.ProjectImages, src => src.Ignore());
        }
    }
}
