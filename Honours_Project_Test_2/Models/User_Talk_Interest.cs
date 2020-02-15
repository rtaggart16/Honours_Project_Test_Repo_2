using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models
{
    public class User_Talk_Interest
    {
        [Key]
        public string ID { get; set; }

        [Required]
        public string User_ID { get; set; }

        [Required]
        public string Talk_ID { get; set; }

        [ForeignKey("User_ID")]
        public virtual Honours_Project_Test_2.Models.User User { get; set; }

        [ForeignKey("Talk_ID")]
        public virtual Talk Talk { get; set; }
    }
}
