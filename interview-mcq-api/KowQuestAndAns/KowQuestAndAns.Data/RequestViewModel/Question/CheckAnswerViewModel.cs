using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.RequestViewModel.Question
{
    public class CheckAnswerViewModel
    {
        public int qeustion_list_id { get; set; }
        public string question_ans_list_id { get; set; }
        public int user_info_id { get; set; }
        public int question_set_id { get; set; }
        public bool timeout { get; set; }
    }
}
