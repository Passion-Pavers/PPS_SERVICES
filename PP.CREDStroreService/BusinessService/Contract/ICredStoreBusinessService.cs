using PP.CREDStroreService.Models.Dtos;

namespace PP.CREDStroreService.BusinessService.Contract
{
    public interface ICredStoreBusinessService
    {
        Task<ResponseDto> AddAsync(CredentialsDto createDto, string userName);
        Task<ResponseDto> DeleteAsync(int id, DeleteCredentialsDto deleteDto);
        Task<ResponseDto> GetAllAsync(GetCredentialsDto request);
        Task<ResponseDto> GetByIdAsync(int id);
        Task<ResponseDto> UpdateAsync(UpdateCredentialDto updateDto, string userName);
    }
}