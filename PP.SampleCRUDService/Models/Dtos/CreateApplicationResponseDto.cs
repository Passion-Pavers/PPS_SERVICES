namespace PP.SampleCRUDService.Models.Dtos
{
    public class CreateApplicationResponseDto
    {
        public int Id { get; set; }

        public string? ApplicationName { get; set; }

        public string? Description { get; set; }
    }
}
