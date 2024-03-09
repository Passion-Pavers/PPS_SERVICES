using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PP.ApplicationService.Models.DbEntities
{
    public class AppDbMapping
    {
        [Key]
        public int AppDBId { get; set; }
        public int AppId { get; set; }
        public int DbId { get; set; }

        [ForeignKey("AppId")]
        public virtual Application Application {get;}

        [ForeignKey("DbId")]
        public virtual AppDataBases AppDataBases { get; set; }
}
}
