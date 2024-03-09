using AutoMapper;
using PP.CREDStroreService.BusinessService.Contract;
using PP.CREDStroreService.Models.DbEntities;
using PP.CREDStroreService.Models.Dtos;
using PP.CREDStroreService.Repository.Contract;

namespace PP.CREDStroreService.BusinessService
{
    public class CredStoreBusinessService : ICredStoreBusinessService
    {
        private readonly ICredDataStoreRepo _credStoreDataRepository;
        private readonly IMapper _mapper;
        private ResponseDto _response;

        public CredStoreBusinessService(ICredDataStoreRepo applicationRepository, IMapper mapper)
        {
            _credStoreDataRepository = applicationRepository;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        public async Task<ResponseDto> GetAllAsync(GetCredentialsDto request)
        {
            var applications = await _credStoreDataRepository.GetAllAsync(request);
            var credDto = _mapper.Map<List<GetCredentialsResponseDto>>(applications);
            _response.Data = credDto;
            return _response;
        }

        public async Task<ResponseDto> GetByIdAsync(int id)
        {
            var application = await _credStoreDataRepository.GetByIdAsync(id);
            var credDto = _mapper.Map<CredentialsDto>(application);
            _response.Data = credDto;
            return _response;
        }

        public async Task<ResponseDto> AddAsync(CredentialsDto createDto, string userName)
        {
            var application = _mapper.Map<Credentials>(createDto, opt =>
            {
                opt.Items.Add("LastModifiedBy", userName);
            });
            await _credStoreDataRepository.AddAsync(application);
            var credDto = _mapper.Map<GetCredentialsResponseDto>(application);
            _response.Data = credDto;
            return _response;
        }

        public async Task<ResponseDto> UpdateAsync(UpdateCredentialDto updateDto, string userName)
        {
            var application = _mapper.Map<Credentials>(updateDto, opt =>
            {
                opt.Items.Add("LastModifiedBy", userName);
            });

            await _credStoreDataRepository.UpdateAsync(application);
           
            _response.Data = updateDto;
            return _response;
        }

        public async Task<ResponseDto> DeleteAsync(int id, DeleteCredentialsDto deleteCredentialsDto)
        {
            await _credStoreDataRepository.DeleteAsync(id);
            _response.Message = $"CredentialId {id} Deleted Successfully";
            return _response;
        }
    }
}
