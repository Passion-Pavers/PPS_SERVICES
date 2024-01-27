using Microsoft.AspNetCore.Identity;

namespace PP.AuthService.Models.DbEntities
{
    public class Users : IdentityUser
    {
        public string Name { get; set; }

    }
}
