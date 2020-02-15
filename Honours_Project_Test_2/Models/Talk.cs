using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models
{
    /// <summary>
    /// Specific details of Talks
    /// </summary>
    public class Talk
    {
        /// <summary>
        /// ID of the talk
        /// </summary>
        [Key]
        public string ID { get; set; }

        /// <summary>
        /// Title of the Talk
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the Talk
        /// </summary>
        public string Description { get; set; }

        [Required]
        public TimeSpan Start { get; set; }

        [Required]
        public TimeSpan End { get; set; }

        /// <summary>
        /// ID of the Author who is performing the Talk
        /// </summary>
        public string Author_ID { get; set; }

        /// <summary>
        /// The number of users that have marked the Talk as interesting
        /// </summary>
        public int Interest_Count { get; set; }

        [ForeignKey("Author_ID")]
        public virtual Author Author { get; set; }

        public ICollection<Talk_Tag> Tags { get; set; }

        public ICollection<Talk_Session> Sessions { get; set; }

        public ICollection<User_Talk_Rating> Ratings { get; set; }
    }
}
