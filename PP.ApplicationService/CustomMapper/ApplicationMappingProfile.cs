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
        }
    }
}