using PP.ApplicationService.Models.Dtos;

namespace PP.ApplicationService.BusinessService.Contract
{
    public interface IApplicationBusinessService
    {
        Task<ResponseDto> GetAllActiveApplications();
    }
}