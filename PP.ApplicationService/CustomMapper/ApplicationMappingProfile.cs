using AutoMapper;
using PP.ApplicationService.Models.DbEntities;
using PP.ApplicationService.Models.Dtos;


namespace PP.ApplicationService.CustomMapper
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Application, ApplicationDto>().ReverseMap();
            CreateMap<Application, CreateApplicationResponseDto>();


            CreateMap<CreateApplicationDto, Application>()
                 .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                 .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                 .ForMember(dest => dest.LastModifiedBy, opt =>
                            opt.MapFrom((src, dest, destMember, context) => context.Items["LastModifiedBy"]));
        }
    }
}