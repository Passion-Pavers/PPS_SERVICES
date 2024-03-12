namespace PP.ApplicationService.Models
{
    public class BaseModel
    {
        public int AppId { get; set; }
        public int SubAppId { get; set; }
        public bool IsPreview { get; set; }
    }
}
