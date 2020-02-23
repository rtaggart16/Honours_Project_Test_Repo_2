using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models.ViewModels.Talk
{
    public class Basic_Talk_Info
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public Session Session { get; set; }

        public int Interest_Count { get; set; }

        public List<decimal> Ratings { get; set; }

        public List<string> Tags { get; set; }

        public Basic_Author_Info Author { get; set; }

        public bool User_Interested { get; set; }

        public decimal User_Rating { get; set; }

        public bool User_Rated { get; set; }
    }
}
