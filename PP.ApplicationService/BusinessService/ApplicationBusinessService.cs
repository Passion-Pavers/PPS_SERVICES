using AutoMapper;
using PP.ApplicationService.BusinessService.Contract;
using PP.ApplicationService.Models.DbEntities;
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
        public async Task<ResponseDto> GetAppConfigJson(int appID, int subAppID)
        {
            var configJson = await _applicationRepository.GetAppConfigJson(appID,subAppID);
            _response.Data = configJson;
            return _response;
        }

        public async Task<ResponseDto> AddApplication(CreateApplicationDto createDto, string userName)
        {
            var application = _mapper.Map<Application>(createDto, opt =>
            {
                opt.Items.Add("LastModifiedBy", userName);
            });
            await _applicationRepository.AddApplicationAsync(application);
            var appDto = _mapper.Map<CreateApplicationResponseDto>(application);
            _response.Data = appDto;
            return _response;
        }
    }
}
