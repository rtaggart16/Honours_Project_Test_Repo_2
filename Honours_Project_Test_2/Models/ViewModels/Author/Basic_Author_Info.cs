using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models.ViewModels.Author
{
    public class Basic_Author_Info
    {
        public string ID { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Username { get; set; }

        public string Photo { get; set; }

        public List<string> Featured_Tags { get; set; }
    }
}
