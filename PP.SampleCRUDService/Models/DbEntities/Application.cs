namespace PP.SampleCRUDService.Models.DbEntities
{
    public class Application
    {
        public int Id { get; set; }
        public string? ApplicationName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }         
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
