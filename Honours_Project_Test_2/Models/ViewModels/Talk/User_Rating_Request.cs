using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models.ViewModels.Talk
{
    public class User_Rating_Request
    {
        public string User_ID { get; set; }

        public string Talk_ID { get; set; }

        public decimal Rating { get; set; }
    }
}
