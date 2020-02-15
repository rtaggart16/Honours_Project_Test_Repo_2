using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models
{
    /// <summary>
    /// Time period that contains Talks
    /// </summary>
    public class Session
    {
        /// <summary>
        /// ID of the session
        /// </summary>
        [Key]
        public string ID { get; set; }

        /// <summary>
        /// Name of the session
        /// </summary>
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        /// <summary>
        /// Description of the session
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Start of the session
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// End of the session
        /// </summary>
        [Required]
        public DateTime End { get; set; }
    }
}
