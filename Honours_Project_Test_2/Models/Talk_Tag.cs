using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models
{
    /// <summary>
    /// Tags that have been assigned to a specific Talk
    /// </summary>
    public class Talk_Tag
    {
        /// <summary>
        /// ID of the Talk-Tag
        /// </summary>
        [Key]
        public string ID { get; set; }

        /// <summary>
        /// ID of the Talk the tag relates to
        /// </summary>
        [Required]
        public string Talk_ID { get; set; }

        /// <summary>
        /// ID of the tag being related to the Talk
        /// </summary>
        [Required]
        public string Tag_ID { get; set; }

        [ForeignKey("Talk_ID")]
        public virtual Talk Talk { get; set; }

        [ForeignKey("Tag_ID")]
        public virtual Tag Tag { get; set; }
    }
}
