using AutoMapper;
using PP.CREDStroreService.Models.DbEntities;
using PP.CREDStroreService.Models.Dtos;


namespace PP.CREDStroreService.CustomMapper
{
    public class CredentialsMappingProfile : Profile
    {
        public CredentialsMappingProfile()
        {
            CreateMap<Credentials, CredentialsDto>();
            CreateMap<Credentials, ResponseDto>();


            CreateMap<CredentialsDto, Credentials>()
                 .ForMember(dest => dest.LastModifedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                 .ForMember(dest => dest.LastModifiedBy, opt =>
                            opt.MapFrom((src, dest, destMember, context) => context.Items["LastModifiedBy"]));


            CreateMap<UpdateCredentialDto, Credentials>()
                .ForMember(dest => dest.LastModifedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                 .ForMember(dest => dest.LastModifiedBy, opt =>
                            opt.MapFrom((src, dest, destMember, context) => context.Items["LastModifiedBy"]));

            CreateMap<Credentials, GetCredentialsResponseDto>();


        }
    }
}