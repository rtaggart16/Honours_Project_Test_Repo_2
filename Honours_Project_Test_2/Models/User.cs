using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models
{
    /// <summary>
    /// Main User class of the application
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// First name of the user
        /// </summary>
        [Required]
        public string First_Name { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        [Required]
        public string Last_Name { get; set; }

        /// <summary>
        /// Specifies if user is an Author
        /// </summary>
        [Required]
        public bool Is_Author { get; set; }
    }
}
