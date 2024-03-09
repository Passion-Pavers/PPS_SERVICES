namespace PP.ApplicationService.Models.DbEntities
{
    public class Application
    {
        public int Id { get; set; }
        public string? ApplicationName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        
    }
}
