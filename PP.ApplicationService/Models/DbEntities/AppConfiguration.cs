using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PP.ApplicationService.Models.DbEntities
{
    public class AppConfiguration
    {
        [Key]
        public int AppConfigId { get; set; }       
        public int AppId { get; set; }        
        public string? AppConfigJson { get; set; }
       
        [ForeignKey(nameof(AppId))]
        public virtual Application Application { get; }
    }
}
