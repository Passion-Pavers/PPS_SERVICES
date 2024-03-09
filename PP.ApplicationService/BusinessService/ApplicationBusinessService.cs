using AutoMapper;
using PP.ApplicationService.BusinessService.Contract;
using PP.ApplicationService.Models.Dtos;
using PP.ApplicationService.Repository.Contract;

namespace PP.ApplicationService.BusinessService
{
    public class ApplicationBusinessService : IApplicationBusinessService
    {
        private readonly IApplicationDataRepo _applicationRepository;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        public ApplicationBusinessService(
            IApplicationDataRepo applicationRepository
           , IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        public async Task<ResponseDto> GetAllActiveApplications()
        {
            var applications = await _applicationRepository.GetAllActiveApplicationsAsync();
            var appResponse = _mapper.Map<List<ApplicationDto>>(applications);
            _response.Data = appResponse;
            return _response;
        }
    }
}
