namespace PP.CREDStroreService.Models.Dtos
{
    public class CredentialDto : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Website { get; set; }
    }
}
