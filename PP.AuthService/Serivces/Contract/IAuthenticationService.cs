using PP.AuthService.Models.Dtos;

namespace PP.AuthService.Serivces.Contract
{
    public interface IAuthenticationService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);

    }
}
