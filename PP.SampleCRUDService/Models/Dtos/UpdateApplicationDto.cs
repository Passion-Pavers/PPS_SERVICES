namespace PP.SampleCRUDService.Models.Dtos
{
    public class UpdateApplicationDto
    {
        public int Id { get; set; }
        public string? ApplicationName { get; set; }
        public string? Description { get; set; }
    }
}
