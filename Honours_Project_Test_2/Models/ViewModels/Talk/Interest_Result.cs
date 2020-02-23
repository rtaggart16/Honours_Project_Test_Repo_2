using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Honours_Project_Test_2.Models.ViewModels.Talk
{
    public class Interest_Result
    {
        public bool Success { get; set; }

        public Basic_Talk_Info Interest_Talk { get; set; }

        public Basic_Talk_Info Overwrite_Talk { get; set; }
    }
}
