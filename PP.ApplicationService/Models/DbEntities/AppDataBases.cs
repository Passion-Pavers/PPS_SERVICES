using System.ComponentModel.DataAnnotations;

namespace PP.ApplicationService.Models.DbEntities
{
    public class AppDataBases
    {
        [Key]
        public int DBId { get; set; }
        public string DBName { get; set; }
    }
}
