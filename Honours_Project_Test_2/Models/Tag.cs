using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models
{
    /// <summary>
    /// Simple name and description to categorise Talks
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// ID of the Tag
        /// </summary>
        [Key]
        public string ID { get; set; }

        /// <summary>
        /// Name of the Tag
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description of the Tag
        /// </summary>
        [Required]
        public string Desciption { get; set; }
    }
}
