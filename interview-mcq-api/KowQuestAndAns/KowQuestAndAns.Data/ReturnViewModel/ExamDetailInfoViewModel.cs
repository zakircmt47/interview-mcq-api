using KowQuestAndAns.Data.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.ReturnViewModel
{
    public class ExamDetailInfoViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone_no { get; set; }
        public string question_name { get; set; }
        public int[] given_answer { get; set; }
        public int[] right_answer { get; set; }
        public string answer { get; set; }
        public string timeout { get; set; }
        public string actual_time { get; set; }
        public string taken_exam_time { get; set; }
        public List<QuestionAnsList> answerList { get; set; }
        public List<QuestionAnsList> given_answer_list { get; set; }
        public List<QuestionAnsList> right_answer_list { get; set; }
        public int total_right_ans { get; set; }
        public int  user_info_id { get; set; }
        public int  total_questions { get; set; }
    }
}
