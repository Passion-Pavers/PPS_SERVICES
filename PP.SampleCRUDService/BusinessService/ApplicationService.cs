using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using PP.SampleCRUDService.BusinessService.Contract;
using PP.SampleCRUDService.Models.DbEntities;
using PP.SampleCRUDService.Models.Dtos;
using PP.SampleCRUDService.Repository.Contract;

namespace PP.SampleCRUDService.BusinessService
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public ApplicationService(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationDto>> GetAllApplications()
        {
           var applications = await _applicationRepository.GetAllAsync();
            return _mapper.Map<List<ApplicationDto>>(applications);
        }

        public async Task<ApplicationDto> GetApplicationById(int id)
        {
           var application = await _applicationRepository.GetByIdAsync(id);
            return _mapper.Map<ApplicationDto>(application);
        }

        public async Task<CreateApplicationResponseDto> AddApplication(CreateApplicationDto createDto, string userName)
        {
            var application = _mapper.Map<Application>(createDto, opt =>
            {
                opt.Items.Add("CreatedBy", userName);
                opt.Items.Add("ModifiedBy", userName);

            });
            await _applicationRepository.AddAsync(application);
            return _mapper.Map<CreateApplicationResponseDto>(application);
        }

        public async Task UpdateApplication(UpdateApplicationDto updateDto, string userName)
        {
            var application = _mapper.Map<Application>(updateDto, opt =>
            {
                opt.Items.Add("CreatedBy", userName);
                opt.Items.Add("ModifiedBy", userName);

            });

            await _applicationRepository.UpdateAsync(application);
        }

        public async Task DeleteApplication(int id)
        {
            await _applicationRepository.DeleteAsync(id);
        }
    }

}
