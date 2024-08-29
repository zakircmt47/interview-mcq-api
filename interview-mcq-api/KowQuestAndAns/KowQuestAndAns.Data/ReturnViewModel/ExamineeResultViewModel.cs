using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.ReturnViewModel
{
    public class ExamineeResultViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string mobile_no { get; set; }
        public string email { get; set; }
        public int result { get; set; }
        public string result_status { get; set; }
        public int total_right_ans { get; set; }
        public int total_wrong_ans { get; set; }
        public int TotalQuestion { get; set; }
    }
}
