using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models.ViewModels.Talk
{
    public class Interest_Request
    {
        [Required]
        public string Talk_ID { get; set; }

        [Required]
        public string User_ID { get; set; }

        [Required]
        public string Overwrite_ID { get; set; }
    }
}
