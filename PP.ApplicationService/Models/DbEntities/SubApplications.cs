using System.ComponentModel.DataAnnotations.Schema;

namespace PP.ApplicationService.Models.DbEntities
{
    public class SubApplications
    {
        public int SubAppID { get; set; }  
        public int AppID { get; set; }     
        public string SubAppName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? AppConfigJson { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }

        [ForeignKey("AppID")]
        public virtual Application Application { get; set; }

    }
}
