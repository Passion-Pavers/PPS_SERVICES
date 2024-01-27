using PP.SampleCRUDService.Models.DbEntities;
using PP.SampleCRUDService.Models.Dtos;

namespace PP.SampleCRUDService.BusinessService.Contract
{
    public interface IApplicationService
    {
        Task<CreateApplicationResponseDto> AddApplication(CreateApplicationDto createDto);
        Task DeleteApplication(int id);
        Task<IEnumerable<ApplicationDto>> GetAllApplications();
        Task<ApplicationDto> GetApplicationById(int id);
        Task UpdateApplication(UpdateApplicationDto updateDto);
    }
}