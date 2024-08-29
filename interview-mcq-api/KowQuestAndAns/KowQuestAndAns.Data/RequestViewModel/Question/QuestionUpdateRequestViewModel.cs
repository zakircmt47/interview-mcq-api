﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.RequestViewModel.Question
{
    public class QuestionUpdateRequestViewModel
    {
        public int question_id { get; set; }
        public int user_info_id { get; set; }
        public int question_set_id { get; set; }
        public int question_subject_id { get; set; }
        public string question_ans_list_id { get; set; }
        public string question_name { get; set; }
        public string question_ans { get; set; }
        public string right_ans { get; set; }

    }
}
