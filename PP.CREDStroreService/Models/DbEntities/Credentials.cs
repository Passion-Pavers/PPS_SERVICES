using System;
using System.ComponentModel.DataAnnotations;

namespace PP.CREDStroreService.Models.DbEntities
{
    public class Credentials
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Website { get; set; }

        public DateTime LastModifedOn { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
