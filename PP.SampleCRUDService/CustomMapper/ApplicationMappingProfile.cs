using AutoMapper;
using PP.SampleCRUDService.Models.DbEntities;
using PP.SampleCRUDService.Models.Dtos;

namespace PP.SampleCRUDService.CustomMapper
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Application, ApplicationDto>();
            CreateMap<Application, CreateApplicationResponseDto>();
            

            CreateMap<CreateApplicationDto, Application>()
                 .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                 .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow));
            
            CreateMap<UpdateApplicationDto, Application>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

        }
    }
}