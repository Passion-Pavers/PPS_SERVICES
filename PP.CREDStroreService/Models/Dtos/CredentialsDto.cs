using System.ComponentModel.DataAnnotations;

namespace PP.CREDStroreService.Models.Dtos
{
    public class CredentialsDto : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string WebSite { get; set; }
    }
}
