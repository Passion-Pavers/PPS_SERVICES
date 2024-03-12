using PP.ApplicationService.Models.Dtos;

namespace PP.ApplicationService.BusinessService.Contract
{
    public interface IApplicationBusinessService
    {
        Task<ResponseDto> GetAllActiveApplications();
        Task<ResponseDto> GetAppConfigJson(int appID, int subAppID);
        Task<ResponseDto> AddApplication(CreateApplicationDto createDto, string userName);
    }
}