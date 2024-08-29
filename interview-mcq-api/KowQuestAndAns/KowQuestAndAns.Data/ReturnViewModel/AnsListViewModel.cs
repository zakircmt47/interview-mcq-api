using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.ReturnViewModel
{
    public class AnsListViewModel
    {
        public int id { get; set; }
        public string question_ans { get; set; }
        public bool isRight { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
    }
}
