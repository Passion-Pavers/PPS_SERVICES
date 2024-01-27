using PP.AuthService.Models.DbEntities;

namespace PP.AuthService.Serivces.Contract
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Users user, IEnumerable<string> roles);
    }
}
