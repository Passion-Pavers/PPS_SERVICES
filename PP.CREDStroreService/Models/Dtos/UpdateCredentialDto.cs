namespace PP.CREDStroreService.Models.Dtos
{
    public class UpdateCredentialDto : BaseModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Website { get; set; } 
    }
}
