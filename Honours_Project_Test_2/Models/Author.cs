using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models
{
    /// <summary>
    /// Specific details of talk authors
    /// </summary>
    public class Author
    {
        /// <summary>
        /// ID of the Author
        /// </summary>
        [Key]
        public string ID { get; set; }

        /// <summary>
        /// ID of the User that the Author is linked to
        /// </summary>
        [Required]
        public string User_ID { get; set; }

        /// <summary>
        /// The profile photo of the Author
        /// </summary>
        [Required]
        public string Profile_Photo { get; set; }

        /// <summary>
        /// Main quote of the Author
        /// </summary>
        [Required]
        public string Featured_Quote { get; set; }

        /// <summary>
        /// The bio of the Author
        /// </summary>
        [Required]
        public string Bio { get; set; }

        [ForeignKey("User_ID")]
        public virtual User User { get; set; }

        public ICollection<Honours_Project_Test_2.Models.Talk> Talks { get; set; }
    }
}
