using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models
{
    /// <summary>
    /// The Session a Talk is part of
    /// </summary>
    public class Talk_Session
    {
        /// <summary>
        /// ID of the Talk-Session
        /// </summary>
        [Key]
        public string ID { get; set; }

        /// <summary>
        /// ID of the Talk
        /// </summary>
        [Required]
        public string Talk_ID { get; set; }

        /// <summary>
        /// ID of the session
        /// </summary>
        [Required]
        public string Session_ID { get; set; }

        [ForeignKey("Talk_ID")]
        public virtual Talk Talk { get; set; }

        [ForeignKey("Session_ID")]
        public virtual Session Session { get; set; }
    }
}
