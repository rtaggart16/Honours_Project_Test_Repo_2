using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models
{
    /// <summary>
    /// Tracks the ratings made by users
    /// </summary>
    public class User_Talk_Rating
    {
        /// <summary>
        /// ID of the User rating
        /// </summary>
        [Key]
        public string ID { get; set; }

        /// <summary>
        /// ID of the user making the rating
        /// </summary>
        [Required]
        public string User_ID { get; set; }

        /// <summary>
        /// ID of the talk being rated
        /// </summary>
        [Required]
        public string Talk_ID { get; set; }

        /// <summary>
        /// Rating value
        /// </summary>
        [Required]
        public decimal Rating { get; set; }

        [ForeignKey("Talk_ID")]
        public virtual Talk Talk { get; set; }

        [ForeignKey("User_ID")]
        public virtual Honours_Project_Test_2.Models.User User { get; set; }
    }
}
